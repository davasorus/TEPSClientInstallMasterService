using System;
using System.Diagnostics;
using System.IO;

namespace TEPSClientInstallService_Master.Classes
{
    public class installerClass
    {
        private loggingClass loggingClass = new loggingClass();

        //this will open files using whatever is the default program associated with the file type.
        //IF there is not a default file processor associated it will display a windows prompt to open the file type
        public async void openProgram(string location, string process)
        {
            try
            {
                var proc = Process.Start(Path.Combine(location, process));
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }
    }
}