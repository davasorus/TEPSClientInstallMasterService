using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    internal class preReqClass
    {
        private loggingClass loggingClass = new loggingClass();

        private sqlServerInteractionClass sqlServerInteractionClass = new sqlServerInteractionClass();

        private bool flag;

        public async Task updatePreReqs()
        {
            try
            {
                string[] test = { "2", "3", "4" };

                foreach (var item in test)
                {
                    string path = "";

                    string[] exec = { item };

                    var test1 = sqlServerInteractionClass.returnSettingsDBValue(exec);

                    foreach (DataRow dr in test1.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr[8].ToString()))
                        {
                            loggingClass.logEntryWriter($"{dr[8]}", "debug");

                            path = Path.Combine(dr[8].ToString(), "_Client-Installation");
                        }
                    }

                    if (Directory.Exists(path))
                    {
                        preReqRename("SSCERuntime_x64-ENU.exe", preReqFileName.sqlCE4064, "SQL Compact Edition 4.0", path);
                        preReqRename("SSCERuntime_x86-ENU.exe", preReqFileName.sqlCE4032, "SQL Compact Edition 4.0", path);

                        await preReqSearchCopy(path, exec[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        #region pre req copy code

        //Mobile copy
        //this will copy a file from one location to another location as sent by PreReqSearch above
        public async void CopyDistributor(string fileNamePath, string ID)
        {
            if (ID.Equals("2"))
            {
                prodCopy(fileNamePath, ID);
            }
            else if (ID.Equals("3"))
            {
                testCopy(fileNamePath, ID);
            }
            else if (ID.Equals("4"))
            {
                trainCopy(fileNamePath, ID);
            }
        }

        private async void prodCopy(string fileNamePath, string ID)
        {
            string replace = "";

            string clientPath = Path.Combine(configValues.clientsStoragePath, "prod");
            string preReqPath = Path.Combine(configValues.preReqStoragePath, "prod");

            Directory.CreateDirectory(preReqPath);
            Directory.CreateDirectory(clientPath);

            List<string> clients = new List<string>()
            {
                clientFileName.mspClient, clientFileName.cadClient64, clientFileName.cadIncObs64, clientFileName.cadClient32
            };

            try
            {
                string filename = Path.GetFileName(fileNamePath);

                foreach (var client in clients)
                {
                    if (filename.Equals(client))
                    {
                        replace = Path.Combine(clientPath, filename);

                        File.Copy(fileNamePath, replace, true);

                        string logEntry1 = filename + " has been copied.";

                        loggingClass.logEntryWriter(logEntry1, "info");

                        string[] executionText = { ID, filename, replace };

                        await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText);

                        return;
                    }
                }

                replace = Path.Combine(preReqPath, filename);

                File.Copy(fileNamePath, replace, true);

                string logEntry = filename + " has been copied.";

                loggingClass.logEntryWriter(logEntry, "info");

                string[] executionText1 = { ID, filename, replace };

                await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText1);
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        private async void testCopy(string fileNamePath, string ID)
        {
            string replace = "";

            string clientPath = Path.Combine(configValues.clientsStoragePath, "test");
            string preReqPath = Path.Combine(configValues.preReqStoragePath, "test");

            Directory.CreateDirectory(preReqPath);
            Directory.CreateDirectory(clientPath);
            List<string> clients = new List<string>()
            {
                clientFileName.mspClient, clientFileName.cadClient64, clientFileName.cadIncObs64, clientFileName.cadClient32
            };

            try
            {
                string filename = Path.GetFileName(fileNamePath);

                foreach (var client in clients)
                {
                    if (filename.Equals(client))
                    {
                        replace = Path.Combine(clientPath, filename);

                        File.Copy(fileNamePath, replace, true);

                        string logEntry1 = filename + " has been copied.";

                        loggingClass.logEntryWriter(logEntry1, "info");

                        string[] executionText = { ID, filename, replace };

                        await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText);

                        return;
                    }
                }

                replace = Path.Combine(preReqPath, filename);

                File.Copy(fileNamePath, replace, true);

                string logEntry = filename + " has been copied.";

                loggingClass.logEntryWriter(logEntry, "info");

                string[] executionText1 = { ID, filename, replace };

                await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText1);
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        private async void trainCopy(string fileNamePath, string ID)
        {
            string replace = "";

            string clientPath = Path.Combine(configValues.clientsStoragePath, "train");
            string preReqPath = Path.Combine(configValues.preReqStoragePath, "train");

            Directory.CreateDirectory(preReqPath);
            Directory.CreateDirectory(clientPath);

            List<string> clients = new List<string>()
            {
                clientFileName.mspClient, clientFileName.cadClient64, clientFileName.cadIncObs64, clientFileName.cadClient32
            };

            try
            {
                string filename = Path.GetFileName(fileNamePath);

                foreach (var client in clients)
                {
                    if (filename.Equals(client))
                    {
                        replace = Path.Combine(clientPath, filename);

                        File.Copy(fileNamePath, replace, true);

                        string logEntry1 = filename + " has been copied.";

                        loggingClass.logEntryWriter(logEntry1, "info");

                        string[] executionText = { ID, filename, replace };

                        await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText);

                        return;
                    }
                }

                replace = Path.Combine(preReqPath, filename);

                File.Copy(fileNamePath, replace, true);

                string logEntry = filename + " has been copied.";

                loggingClass.logEntryWriter(logEntry, "info");

                string[] executionText1 = { ID, filename, replace };

                await sqlServerInteractionClass.checkForPreReq("GetPreReqByName", executionText1);
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        #endregion pre req copy code

        #region pre req search/rename/copy

        //this is designed to relabel the SQL compact 4.0 64bit and 32bit components.
        //this is so that CAD and the other applications will be able to have the correct pre reqs
        public async void preReqRename(string FileName, string NewName, string SubFolderSearch, string folderPath)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(folderPath))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        string sourcepath = Path.GetDirectoryName(filename);

                        //if the directory of a found file contains the text of a searched term/s
                        //then a specific file is renamed to a desired name.
                        if (sourcepath.ToString().Contains(SubFolderSearch))
                        {
                            File.Move(Path.Combine(sourcepath, FileName), Path.Combine(sourcepath, NewName));

                            string logEntry = FileName + " has been renamed to" + NewName;

                            loggingClass.logEntryWriter(logEntry, "info");
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (IOException ex)
            {
            }
            catch (Exception ex)
            {
                //if the exception error contains the text file not found (system.IO.FileNotFound)
                //then specific text is entered into the log file.
                if (ex.ToString().Contains("FileNotFound"))
                {
                }
                else if (ex.Message.Contains("Could not find a part of the path"))
                {
                }
                else if (ex.Message.Contains("The UNC path should be of the form"))
                {
                    string logEntry = $"Please verify {folderPath} it may be incorrect.";

                    loggingClass.logEntryWriter(logEntry, "debug");
                }
                else if (ex.Message.Contains("Access to the path is denied."))
                {
                    string logEntry = $"You're user is unable to rename a file. Please ensure your user account has full right to {folderPath}";

                    loggingClass.logEntryWriter(logEntry, "debug");
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The given path's format is not supported"))
                {
                }
                else if (ex.Message.Contains("Value cannot be null"))
                {
                    string logEntry = $"Please verify {folderPath} it may be incorrect.";

                    loggingClass.logEntryWriter(logEntry, "debug");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //this searches through a user entered directory/subdirectories for pre reqs
        public async Task preReqSearchCopy(string sDir, string ID)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        CopyDistributor(filename, ID);
                    }

                    //this is so that a folder that has a subdirectory will also be searched
                    preReqSearchCopy(directory, ID);
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        //will search for different versions of applications
        //Primarily for .net
        public int preReqSearch(string sDir, string preReqName)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        if (Path.GetFileName(filename) == preReqName)
                        {
                            //if the file name of the found file within the folders/subdirs matches the one file we are searching for
                            /// the flag is set to true and we exit this level of recursion
                            flag = true;
                            break;
                        }
                    }
                    //due to the multi leveled recursion we must check to see if the bool flag has the value of true (or doesn't have the value of false)
                    //IF the flag is false then the recursion is called again to check the subdir that returned false
                    //if the flag is set to not false then the recursion escapes to the next level
                    if (flag == false)
                    {
                        preReqSearch(directory, preReqName);
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The network path was not found"))
                {
                    loggingClass.logEntryWriter(ex.Message, "error");

                    loggingClass.nLogLogger($"Unable to locate {preReqName} at {sDir}, please ensure the path is correct.", "debug");
                }
                else if (ex.Message.Contains("The UNC path should be of the form"))
                {
                    string logEntry = $"Please verify {sDir} it may be incorrect.";

                    loggingClass.logEntryWriter(logEntry, "debug");
                }
                else if (ex.Message.Contains("The user name or password is incorrect"))
                {
                    loggingClass.logEntryWriter("There was an error searching for" + preReqName + " over the network. Please verify your user can, or download pre reqs locally.", "error");

                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("The given path's format is not supported"))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");
                }
                else if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.logEntryWriter(ex.Message, "error");

                    loggingClass.nLogLogger($"Unable to locate {sDir}, please ensure the path is correct.", "debug");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "info");

                    //loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), logEntry, "Automated Error Reported by " + Environment.UserName);
                }
            }

            //this code block is so that a band aide escape of the multi leveled recursion
            //if the bool has a value we want to maintain that value.
            //   False = 0 and not false = 1
            if (flag == false)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion pre req search/rename/copy
    }
}