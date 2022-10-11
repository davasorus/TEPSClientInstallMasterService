using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    internal class remoteConnectionClass
    {
        private static string applicationName = "TEPS Automated Client Install Master Service " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private readonly string logFileName = $@"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Master-Service\Logging\{applicationName}.json";

        private loggingClass loggingClass = new loggingClass();
        private static bool resolveNames;
        private static object lockObj;
        private static int upCount;

        public async Task getRemoteMachines()
        {
        }
    }
}

public class netWorkMachines
{
    public static List<string> names = new List<string>();
}