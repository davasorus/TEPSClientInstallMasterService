using System.ServiceProcess;

namespace TEPSClientInstallService_Master
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TEPSClientInstallServiceMaster()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}