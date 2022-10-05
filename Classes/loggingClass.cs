using NLog;
using NLog.Config;
using NLog.Targets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    public class loggingClass
    {
        private static Logger _logger;

        private static sqlServerInteraction sqlServerInteraction = new sqlServerInteraction();

        private static string applicationName = "TEPS Automated Client Install Master Service " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private readonly string logFileName = $@"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Logging\{applicationName}.json";

        //adds log messages to log collection (which is then seen via the internal log viewer view)
        public async void logEntryWriter(string logMessage, string level)
        {
            //this.Dispatcher.Invoke(() => logs.Collection.Add(new loggingObj { Message = logMessage, Date = DateTime.Now }));

            nLogLogger(logMessage, level);

            if (level.Equals("error"))
            {
                await submitSQLError(logMessage);
            }
            
        }

        private async Task submitSQLError(string logMessage)
        {
            string[] executionText = { logMessage };

            sqlServerInteraction.executeNonReturningStoredProcedure("InsertErrorLog", executionText);
        }

        public void initializeNLogLogger()
        {
            var config = new LoggingConfiguration();

            var target =
                new FileTarget
                {
                    FileName = logFileName,
                    ArchiveAboveSize = 5000000,
                    ArchiveNumbering = ArchiveNumberingMode.Sequence
                };

            config.AddTarget("logfile", target);

            var rule = new LoggingRule("*", LogLevel.Debug, target);

            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }

        //more performant logging for moving files
        public void nLogLogger(string message, string level)
        {
            _logger = LogManager.GetLogger(level);

            switch (level)
            {
                case "info":
                    _logger.Info(message);
                    break;

                case "debug":
                    _logger.Debug(message);
                    break;

                case "error":
                    _logger.Error(message);
                    break;

                default:
                    break;
            }
        }
    }
}