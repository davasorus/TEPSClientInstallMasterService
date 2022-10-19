using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

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

                        await oriFinderAsync(path, exec[0]);
                    }
                    else if (Directory.Exists(path1))
                    {
                        await oriFinderAsync(path, exec[0]);
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
                        //await preReqSearchCopy(path, exec[0]);

                        await fdidSearchAsync(path, exec[0]);
                    }
                    else if (Directory.Exists(path1))
                    {
                        await fdidSearchAsync(path, exec[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        //searches through the aegis mobile folder to count all ORI folders
        public async Task oriFinderAsync(string location, string ID)
        {
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
                                            string[] executionText = { sub, ID };

                                            await sqlServerInteractionClass.checkForAgency("GetORIByNameByEnrolledInstanceType", executionText);

                                            loggingClass.logEntryWriter($"{sub} was found", "debug");
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
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");
            }
        }

        //searches for ORIs on the remote server
        //copies them to the local server
        public async Task fdidSearchAsync(string location, string ID)
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

            string sDir = location;

            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    string[] subs = directory.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string[] fdidName = { NUM1, NUM2, NUM3, NUM4, NUM5, NUM6, NUM7, NUM8, NUM9, NUM10 };

                    foreach (var sub in subs)
                    {
                        foreach (var name in fdidName)
                        {
                            if (sub.StartsWith(name))
                            {
                                string[] executionText = { sub, ID };

                                await sqlServerInteractionClass.checkForAgency("GetFDIDByNameByEnrolledInstanceType", executionText);

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
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The process cannot access the file"))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("Access to the path"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else if (ex.Message.Contains("The network path was not found"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else if (ex.Message.Contains("The user name or password is incorrect."))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                }
            }
        }
    }
}