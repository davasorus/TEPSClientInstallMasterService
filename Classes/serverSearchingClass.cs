namespace TEPSClientInstallService_Master.Classes
{
    internal class serverSearchingClass
    {
        private loggingClass loggingClass = new loggingClass();

        private sqlServerInteractionClass sqlServerInteractionClass = new sqlServerInteractionClass();

        public async Task searchForORI()
        {
            try
            {
                string[] test = { "2", "3", "4" };

                foreach (var item in test)
                {
                    string path = "";
                    string path1 = "";

                    string[] exec = { item };

                    var test1 = sqlServerInteractionClass.returnSettingsDBValue(exec);

                    foreach (DataRow dr in test1.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr[2].ToString()))
                        {
                            path = Path.Combine(dr[2].ToString(), @"New World Systems\Aegis Mobile");
                            path1 = Path.Combine(@"\\" + dr[2].ToString(), @"C$\C$\Programdata\New World Systems\Aegis Mobile\Data");
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        //await preReqSearchCopy(path, exec[0]);

                        await oriFinderAsync(Path, exec[0]);
                    }
                    else if(Directory.Exists(path1))
                    {
                        await oriFinderAsync(Path, exec[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        public async Task searchForFDID()
        {
            try
            {
                string[] test = { "2", "3", "4" };

                foreach (var item in test)
                {
                    string path = "";
                    string path1 = "";

                    string[] exec = { item };

                    var test1 = sqlServerInteractionClass.returnSettingsDBValue(exec);

                    foreach (DataRow dr in test1.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr[2].ToString()))
                        {
                            path = Path.Combine(dr[2].ToString(), @"New World Systems\Aegis Mobile");
                            path1 = Path.Combine(@"\\" + dr[2].ToString(), @"C$\C$\Programdata\New World Systems\Aegis Mobile\Data");
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        await fdidSearchAsync(Path, exec[0]);
                    }
                    else if(Directory.Exists(path1))
                    {
                        await fdidSearchAsync(Path, exec[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

            //searches through the aegis mobile folder to count all ORI folders
        public async Task oriFinderAsync(string location, string[] executionText)
        {
            numOfORIs = 0;

            string[] states = {"AL","AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY",
        "LA","ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NB", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI",
        "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "TP", "NWS"};

            string sDir = location;

            try
            {
                if (Directory.Exists(sDir))
                {
                    foreach (var directory in Directory.GetDirectories(sDir))
                    {
                        string dirName = Path.GetFullPath(directory);
                        char[] separators = new char[] { '\\' };
                        string[] subs = dirName.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var sub in subs)
                        {
                            foreach (var state in states)
                            {
                                if (sub.Contains(state))
                                {
                                    try
                                    {
                                        string sDir2 = Path.Combine(dirName, "Forms");
                                        string[] files = Directory.GetFiles(sDir2);
                                        if (files.Length > 0)
                                        {
                                            //some stored procedure that updates the ORI DB Table at the enrolled instance
                                            loggingClass.logEntryWriter($"{sub} was found", "debug");

                                            return;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    numOfORIs = 0;
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");
                loggingClass.ezViewLogWriter(logEntry);
                await loggingClass.remoteErrorReporting("Server Migration Utility", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                numOfORIs = 0;
            }
        }


         //searches for ORIs on the remote server
        //copies them to the local server
        public async Task fdidSearchAsync(string server1, string[] executionText)
        {
            string NUM1 = "1";
            string NUM2 = "2";
            string NUM3 = "3";
            string NUM4 = "4";
            string NUM5 = "5";
            string NUM6 = "6";
            string NUM7 = "7";
            string NUM8 = "8";
            string NUM9 = "9";
            string NUM10 = "0";

            char[] separators = new char[] { '\\' };

            sDir1 = @"\\" + server1 + @"\C$\ProgramData\New World Systems\Aegis Mobile\Data";

            try
            {
                foreach (var directory in Directory.GetDirectories(sDir1))
                {
                    string[] subs = directory.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string[] fdidName = { NUM1, NUM2, NUM3, NUM4, NUM5, NUM6, NUM7, NUM8, NUM9, NUM10 };

                    foreach (var sub in subs)
                    {
                        foreach (var name in fdidName)
                        {
                            if (sub.StartsWith(name))
                            {
                                 //some stored procedure that updates the FDID DB Table at the enrolled instance
                                 loggingClass.logEntryWriter($"{sub} was found", "debug");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.ezViewLogWriter("Debug: ERROR when searching for FDIDs check Migration log file.");

                    loggingClass.logTextWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The process cannot access the file"))
                {
                    loggingClass.ezViewLogWriter("ERROR-> Unable to access a file, please check Migration log file.");

                    loggingClass.logTextWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("Access to the path"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    loggingClass.ezViewLogWriter($"ERROR-> Unable to access {sDir1}, please ensure you're user can UNC path to it.");
                }
                else if (ex.Message.Contains("The network path was not found"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    loggingClass.ezViewLogWriter($"Unable to access {sDir1}, please ensure the server name is correct.");
                }
                else if (ex.Message.Contains("The user name or password is incorrect."))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    loggingClass.ezViewLogWriter($"ERROR-> Unable to access {sDir1}, please ensure your user can UNC Path.");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    loggingClass.ezViewLogWriter("ERROR-> " + logEntry);

                    await loggingClass.remoteErrorReporting("Server Migration Utility", Assembly.GetExecutingAssembly().GetName().Version.ToString(), logEntry, "Automated Error Reported by " + Environment.UserName);
                }
            }
        }
    }
}