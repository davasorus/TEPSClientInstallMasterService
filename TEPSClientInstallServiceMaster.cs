using System;
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
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();

        private preReqClass preReqClass = new preReqClass();
        private serverSearchingClass serverSearchingClass = new serverSearchingClass();

        public TEPSClientInstallServiceMaster()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            loggingClass.initializeNLogLogger();

            getConfigValues();

            var config = new HttpSelfHostConfiguration($"http://{Environment.MachineName}:8081");

            var cors = new EnableCorsAttribute($"*", "*", "*");

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

            Timer updateCheckTimer = new Timer
            {
                Interval = 600000
            };
            updateCheckTimer.Start();

            updateCheckTimer.Elapsed += updateCheckTimer_Elapsed;

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
                installerClass.openProgram(configValues.updaterStoragePath, "TEPS Automated Master Service Updater.exe");
            }

            Task task1 = Task.Factory.StartNew(() => updatePreReqs());
        }

        private void updateCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!File.Exists(Path.Combine(configValues.updaterStoragePath, "TEPS Automated Master Service Updater.exe")))
            {
                File.Move(Path.Combine(configValues.serviceRunPath, "TEPS Automated Master Service Updater.exe"), Path.Combine(configValues.updaterStoragePath, "TEPS Automated Master Service Updater.exe"));
            }

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram(configValues.updaterStoragePath, "TEPS Automated Master Service Updater.exe");
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

        private async Task updatePreReqs()
        {
            await preReqClass.updatePreReqs();
            await serverSearchingClass.searchForORI();
            await serverSearchingClass.searchForFDID();
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

internal class preReqFileName
{
    public static readonly string dotNet47 = "dotNetFx471_Full_setup_Offline.exe";
    public static readonly string dotNet48 = "ndp48-x86-x64-allos-enu.exe";
    public static readonly string sqlCE3532 = "SSCERuntime_x86-ENU.msi";
    public static readonly string sqlCE3564 = "SSCERuntime_x64-ENU.msi";
    public static readonly string sqlCE4032 = "SSCERuntime_x86-ENU-4.0.exe";
    public static readonly string sqlCE4064 = "SSCERuntime_x64-ENU-4.0.exe";
    public static readonly string nwpsGis32 = "NewWorld.Gis.Components.x86.msi";
    public static readonly string nwpsGis64 = "NewWorld.Gis.Components.x64.msi";
    public static readonly string msSync64 = "Synchronization-v2.1-x64-ENU.msi";
    public static readonly string msProServ64 = "ProviderServices-v2.1-x64-ENU.msi";
    public static readonly string msDbPro64 = "DatabaseProviders-v3.1-x64-ENU.msi";
    public static readonly string msSync32 = "Synchronization-v2.1-x86-ENU.msi";
    public static readonly string msProServ32 = "ProviderServices-v2.1-x86-ENU.msi";
    public static readonly string msDbPro32 = "DatabaseProviders-v3.1-x86-ENU.msi";
    public static readonly string nwpsUpdate = "NewWorld.Management.Updater.msi";
    public static readonly string sqlClr32 = "SQLSysClrTypesx86.msi";
    public static readonly string sqlClr64 = "SQLSysClrTypesx64.msi";
    public static readonly string sqlClr201232 = "SQLSysClrTypesx2012.msi";
    public static readonly string sqlClr201264 = "SQLSysClrTypesx642012.msi";
    public static readonly string SCPD6 = "SPD6-4-8993.exe";
    public static readonly string SCPD6AX = "SPDX6-4-3091.exe";
    public static readonly string SCPD4 = "SPD4-0-92.exe";
}

internal class clientFileName
{
    public static readonly string mspClient = "NewWorldMSPClient.msi";
    public static readonly string cadClient64 = "NewWorld.Enterprise.CAD.Client.x64.msi";
    public static readonly string cadClient32 = "NewWorld.Enterprise.CAD.ManagementClient.x64.msi";
    public static readonly string cadIncObs64 = "NewWorld.Enterprise.CAD.IncidentObserver.x64.msi";
}