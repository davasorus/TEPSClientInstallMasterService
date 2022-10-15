﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using TEPSClientInstallService_Master.Classes;

namespace TEPSClientInstallService_Master
{
    public partial class TEPSClientInstallServiceMaster : ServiceBase
    {
        private loggingClass loggingClass = new loggingClass();
        private installerClass installerClass = new installerClass();
        private remoteConnectionClass remoteConnectionClass = new remoteConnectionClass();
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();

        public TEPSClientInstallServiceMaster()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            loggingClass.initializeNLogLogger();

            getConfigValues();

            var config = new HttpSelfHostConfiguration($"http://{Environment.MachineName}:8081");

            var cors = new EnableCorsAttribute($"http://{Environment.MachineName}", "*", "*");

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            loggingClass.logEntryWriter("Service is started at " + DateTime.Now, "info");
            loggingClass.logEntryWriter($"API listening at {config.BaseAddress}", "info");

            Timer timer = new Timer
            {
                Interval = 600000
            };
            timer.Start();

            timer.Elapsed += Timer_Elapsed;

            Directory.CreateDirectory(configValues.updaterStoragePath);
            Directory.CreateDirectory(configValues.addonStoragePath);
            Directory.CreateDirectory(configValues.clientsStoragePath);
            Directory.CreateDirectory(configValues.preReqStoragePath);

            File.Copy(Path.Combine(configValues.serviceRunPath, "TEPS Automated Master Service Updater.exe"), Path.Combine(configValues.updaterStoragePath, "TEPS Automated Master Service Updater.exe"), true);

            Process[] localbyName = Process.GetProcessesByName("TEPS Client Install Master Service Updater Utility");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\Updater", "TEPS Automated Master Service Updater.exe");
            }

            Task task1 = Task.Factory.StartNew(() => getNetworkMap());
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!File.Exists(Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\Updater", "TEPS Automated Master Service Updater.exe")))
            {
                File.Move(Path.Combine("C:\\Services\\Tyler-Client-Install-Master-Service", "TEPS Automated Master Service Updater.exe"), Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\Updater", "TEPS Automated Master Service Updater.exe"));
            }

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\Updater", "TEPS Automated Master Service Updater.exe");
            }
        }

        protected override void OnStop()
        {
            loggingClass.logEntryWriter("Service is stopped at " + DateTime.Now, "info");

            loggingClass.logEntryWriter($"API no longer listening", "info");
        }

        private void getConfigValues()
        {
            var dbName = ConfigurationManager.AppSettings["DBName"];

            if (dbName == null || dbName == "")
            {
                loggingClass.logEntryWriter("DBName config is blank. Please enter in DB name and instance and attempt to start", "error");

                throw new Exception();
            }
            else
            {
                configValues.DBName = dbName;
            }
        }

        private async Task getNetworkMap()
        {
            await remoteConnectionClass.getRemoteMachines();

            foreach (var item in netWorkMachines.names)
            {
                //string[] executionText = { item.ToString() };

                //sqlServerInteraction.checkForClient("GetClientByName", executionText);
            }
        }
    }
}

internal class configValues
{
    public static string DBName { get; set; }

    public static readonly string updaterStoragePath = "C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\Updater";
    public static readonly string addonStoragePath = "C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\File Repository\\Addons";
    public static readonly string clientsStoragePath = "C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\File Repository\\Clients";
    public static readonly string preReqStoragePath = "C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Master-Service\\File Repository\\Pre Reqs";
    public static readonly string serviceRunPath = "C:\\Services\\Tyler-Client-Install-Master-Service";

    public static readonly string applicationName = "TEPS Automated Client Install Master Service " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
    public static readonly string logFileName = $@"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Master-Service\Logging\{applicationName}.json";
}