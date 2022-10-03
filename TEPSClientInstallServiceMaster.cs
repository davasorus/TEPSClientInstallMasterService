﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using System.Web.Http;
using System.Web.Http.SelfHost;
using TEPSClientInstallService_Master.Classes;

namespace TEPSClientInstallService_Master
{
    public partial class TEPSClientInstallServiceMaster : ServiceBase
    {
        private loggingClass loggingClass = new loggingClass();
        private installerClass installerClass = new installerClass();
        private sqlServerInteraction sqlServerInteraction = new sqlServerInteraction();

        public TEPSClientInstallServiceMaster()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            loggingClass.initializeNLogLogger();

            getConfigValues();

            var config = new HttpSelfHostConfiguration($"http://{Environment.MachineName}:8081");

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

            //timer.Elapsed += Timer_Elapsed;

            //Directory.CreateDirectory("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater");

            //File.Copy(Path.Combine("C:\\Services\\Tyler-Client-Install-Agent", "TEPS Automated Agent Updater.exe"), Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe"), true);

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                //installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe");
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!File.Exists(Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe")))
            {
                File.Move(Path.Combine("C:\\Services\\Tyler-Client-Install-Agent", "TEPS Automated Agent Updater.exe"), Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe"));
            }

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe");
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
    }
}

internal class configValues
{
    public static string DBName { get; set; }
}