using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    public class utilityClass
    {
        private loggingClass loggingClass = new loggingClass();
        private sqlServerInteractionClass sqlServerInteractionClass = new sqlServerInteractionClass();

        public async Task parseJsonForMessage(string clientName, int EnrolledInstanceType, string message)
        {
            var data = JsonConvert.DeserializeObject<List<tupleData>>(message);

            foreach (var item in data)
            {
                if (item.responseCode.Equals("400 Bad Request"))
                {
                    if (item.message.Contains("- Uninstalled"))
                    {
                        await loggingClass.submitSQLUninstallInstallLog(clientName, EnrolledInstanceType, item.message);
                        await updateInstalledCatalog(item.message, clientName);
                    }
                    else if (item.message.Contains("not found on machine"))
                    {
                        await loggingClass.submitSQLUninstallInstallLog(clientName, EnrolledInstanceType, item.message);
                        await updateInstalledCatalog(item.message, clientName);
                    }
                    else
                    {
                        await loggingClass.submitSQLError(item.message, clientName);
                        await updateInstalledCatalog(item.message, clientName);
                    }
                }
                if (item.responseCode.Equals("200 OK"))
                {
                    if (item.message.Contains("- Uninstalled"))
                    {
                        await loggingClass.submitSQLUninstallInstallLog(clientName, EnrolledInstanceType, item.message);
                        await updateInstalledCatalog(item.message, clientName);
                    }
                    else
                    {
                        await loggingClass.submitSQLInstallLog(clientName, EnrolledInstanceType, item.message);
                        await updateInstalledCatalog(item.message, clientName);
                    }
                }
            }
        }

        public int parseRequestBodyEnrolledInstanceType(string body)
        {
            int foundItem = 0;

            var grabbedItem = "";
            try
            {
                JArray jArray = JArray.Parse(body);

                var jsonObjects = jArray.OfType<JObject>().ToList();

                foreach (JObject jObject in jsonObjects)
                {
                    grabbedItem = jObject.GetValue("Instance").ToString();
                }

                int y = int.Parse(grabbedItem.ToString());

                foundItem = y;
            }
            catch (Exception ex)
            {
                try
                {
                    dynamic jsonObj = JsonConvert.DeserializeObject(body);

                    return jsonObj["Instance"];
                }
                catch (Exception ex1)
                {
                    loggingClass.logEntryWriter(ex1.ToString(), "error");
                }
            }

            return foundItem;
        }

        public string parseRequestBodyFileName(string body)
        {
            string foundItem = "";
            var grabbedItem = "";
            try
            {
                JArray jArray = JArray.Parse(body);

                var jsonObjects = jArray.OfType<JObject>().ToList();

                foreach (JObject jObject in jsonObjects)
                {
                    grabbedItem = jObject.GetValue("FileName").ToString();
                }

                foundItem = grabbedItem;
            }
            catch (Exception ex)
            {
                try
                {
                    dynamic jsonObj = JsonConvert.DeserializeObject(body);

                    return jsonObj["FileName"];
                }
                catch (Exception ex1)
                {
                    loggingClass.logEntryWriter(ex1.ToString(), "error");

                    foundItem = null;
                }
            }

            return foundItem;
        }

        public async Task updateInstalledCatalog(string message, string clientName)
        {
            List<string> exec1 = new List<string>();
            DataTable clientByNameTable = new DataTable();
            string[] exec = { clientName };
            int bit = 0;

            clientByNameTable = sqlServerInteractionClass.executeReturningStoredProcedure("GetClientByName", exec);

            if (clientByNameTable.Rows.Count > 0)
            {
                foreach (DataRow row in clientByNameTable.Rows)
                {
                    exec1.Add(row[0].ToString());
                }
            }
            else
            {
                sqlServerInteractionClass.checkForCatalog("GetInstalledCatalogByID", exec);
            }

            if (message.Contains("not found on machine") || message.Contains("- Uninstalled") || message.Contains("failed to install"))
            {
                bit = 0;

                exec1.Add(bit.ToString());
                string t = message;

                switch (message)
                {
                    case string a when message.Contains("Microsoft SQL Server Compact 3.5 SP2 x64 ENU"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp3564", exec1.ToArray());

                        break;

                    case string a when message.Contains("Microsoft SQL Server Compact 3.5 SP2"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp3532", exec1.ToArray());

                        break;

                    case string a when message.Contains("New World GIS Components x64"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogGISComp64", exec1.ToArray());

                        break;

                    case string a when message.Contains("New World GIS Components x86"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogGISComp32", exec1.ToArray());

                        break;

                    case string a when message.Contains("Updater"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogUpdater", exec1.ToArray());

                        break;

                    case string a when message.Contains("Microsoft SQL Server Compact 4.0 x64 ENU"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp0464", exec1.ToArray());

                        break;

                    case string a when message.Contains("Microsoft SQL Server System CLR Types (x64)"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR200864", exec1.ToArray());

                        break;

                    case string a when message.Contains("Microsoft SQL Server System CLR Types"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR200832", exec1.ToArray());

                        break;

                    case string a when message.Contains("MSP") || message.Contains("Aegis"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogLERMS", exec1.ToArray());

                        break;

                    case string a when message.Contains("Incident Observer"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogCADObserver", exec1.ToArray());

                        break;

                    case string a when message.Contains("CAD"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogCAD", exec1.ToArray());

                        break;

                    case string a when message.Contains("SQL Compact 4.0"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp0464", exec1.ToArray());

                        break;

                    case string a when message.Contains("CLR Types 2012"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR201232", exec1.ToArray());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR201264", exec1.ToArray());

                        break;

                    case string a when message.Contains("ScenePD 6") || message.Contains("ScenePD 4"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogScenePD", exec1.ToArray());

                        break;

                    case string a when message.Contains("Fire"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogFireMobile", exec1.ToArray());

                        break;

                    case string a when message.Contains("Law"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogLEMobile", exec1.ToArray());

                        break;

                    case string a when message.Contains("Merge"):
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogMergeMobile", exec1.ToArray());

                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (message)
                {
                    case string t when message.Contains("DotNet"):
                        bit = 1;

                        exec1.Add(bit.ToString());

                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogDotNet", exec1.ToArray());

                        break;

                    case string t when message.Contains("SQL Compact 3.5") || message.Contains("Microsoft SQL Server Compact 3.5 SP2"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp3532", exec1.ToArray());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp3564", exec1.ToArray());
                        break;

                    case string t when message.Contains("SQL Compact 4.0") || message.Contains("Microsoft SQL Server Compact 4.0 x64"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLComp0464", exec1.ToArray());
                        break;

                    case string t when message.Contains("GIS Components"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogGISComp64", exec1.ToArray());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogGISComp32", exec1.ToArray());
                        break;

                    case string t when message.Contains("DB Providers"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogDBProvider", exec1.ToArray());

                        break;

                    case string t when message.Contains("Updater"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogUpdater", exec1.ToArray());

                        break;

                    case string t when message.Contains("2008 CLR Types"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR200864", exec1.ToArray());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR200832", exec1.ToArray());
                        break;

                    case string t when message.Contains("2012 CLR Types"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR201232", exec1.ToArray());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogSQLCLR201264", exec1.ToArray());
                        break;

                    case string t when message.Contains("LERMS"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogLERMS", exec1.ToArray());

                        break;

                    case string a when message.Contains("Incident Observer"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogCADObserver", exec1.ToArray());

                        break;

                    case string t when message.Contains("CAD"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogCAD", exec1.ToArray());

                        break;

                    case string t when message.Contains("ORI"):

                        exec1.Add(message);
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogMobileConfig", exec1.ToArray());

                        break;

                    case string t when message.Contains("FDID"):

                        exec1.Add(message);
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogMobileConfig", exec1.ToArray());

                        break;

                    case string t when message.Contains("Police Client"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogLEMobile", exec1.ToArray());

                        break;

                    case string t when message.Contains("Fire Client"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogFireMobile", exec1.ToArray());

                        break;

                    case string t when message.Contains("Merge Client"):
                        bit = 1;

                        exec1.Add(bit.ToString());
                        sqlServerInteractionClass.executeNonReturningStoredProcedure("UpdateCatalogMergeMobile", exec1.ToArray());

                        break;
                }
            }
        }

        public async Task configureSettingsTableAsync(string body)
        {
            var MobileServer = "";
            var ESSServer = "";
            var RecordsServer = "";
            var CADServer = "";
            var GISServer = "";
            var GISInstance = "";
            var Instance = "";
            var ClientPath = "";

            try
            {
                //var jsonFilePackage = JsonConvert.DeserializeObject<serverConfigObj>(body);

                dynamic jsonObj = JsonConvert.DeserializeObject(body);

                MobileServer = jsonObj["MobileServer"];
                ESSServer = jsonObj["ESSServer"];
                RecordsServer = jsonObj["RecordsServer"];
                CADServer = jsonObj["CADServer"];
                GISServer = jsonObj["GISServer"];
                GISInstance = jsonObj["GISInstance"];
                Instance = jsonObj["Instance"];
                ClientPath = jsonObj["ClientInstallationPath"];

                string[] execcutionText = { MobileServer, ESSServer, RecordsServer, CADServer, GISServer, GISInstance, Instance, ClientPath };

                sqlServerInteractionClass.checkForSettings("", execcutionText);

                parseORI(body);

                parseFDID(body);

                return;
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }

            try
            {
                JArray jArray = JArray.Parse(body);

                var jsonObjects = jArray.OfType<JObject>().ToList();

                foreach (JObject jObject in jsonObjects)
                {
                    MobileServer = jObject.GetValue("MobileServer").ToString();

                    ESSServer = jObject.GetValue("ESSServer").ToString();

                    RecordsServer = jObject.GetValue("RecordsServer").ToString();

                    CADServer = jObject.GetValue("CADServer").ToString();

                    GISServer = jObject.GetValue("GISServer").ToString();

                    GISInstance = jObject.GetValue("GISInstance").ToString();

                    Instance = jObject.GetValue("Instance").ToString();

                    ClientPath = jObject.GetValue("Client-Installation-Path").ToString();
                }

                string[] execcutionText = { MobileServer, ESSServer, RecordsServer, CADServer, GISServer, GISInstance, Instance, ClientPath };

                sqlServerInteractionClass.checkForSettings("", execcutionText);
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        private async Task<string> parseORI(string body)
        {
            //serverConfigObj.configFileORIObjs.Clear();

            try
            {
                for (var i = 1; i < body.Length; i++)
                {
                    string txt = $"ORI{i}";

                    var jsonFilePackage = (JObject)JsonConvert.DeserializeObject(body);

                    JToken oriToken = jsonFilePackage.SelectToken("PoliceList[?(@.FieldName == '" + txt + "')]");

                    if (oriToken != null)
                    {
                        string ori = oriToken.Value<string>("ORI");

                        // serverConfigObj.configFileORIObjs.Add(new jsonConfigFileORIObj { FieldName = txt, ORI = ori });
                    }
                    else
                    {
                        i = body.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                string logEntry1 = ex.ToString();

                loggingClass.logEntryWriter(logEntry1, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                return "error";
            }

            return "";
        }

        private async Task<string> parseFDID(string body)
        {
            //serverConfigObj.configFileFDIDObjs.Clear();

            try
            {
                for (var i = 1; i < body.Length; i++)
                {
                    string txt = $"FDID{i}";

                    var jsonFilePackage = (JObject)JsonConvert.DeserializeObject(body);

                    JToken fdidToken = jsonFilePackage.SelectToken("FireList[?(@.FieldName == '" + txt + "')]");

                    if (fdidToken != null)
                    {
                        string fdid = fdidToken.Value<string>("FDID");

                        //serverConfigObj.configFileFDIDObjs.Add(new jsonConfigFileFDIDObj { FieldName = txt, FDID = fdid });
                    }
                    else
                    {
                        i = body.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                string logEntry1 = ex.ToString();

                loggingClass.logEntryWriter(logEntry1, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                return "error";
            }

            return "";
        }

        public async Task<string> grabsettingsByInstanceID(int id)
        {
            string[] exec = { id.ToString() };
            string data = "";
            DataTable dt = new DataTable();

            dt = sqlServerInteractionClass.executeReturningStoredProcedure("GetSettingByInstance", exec);

            data = JsonConvert.SerializeObject(dt);

            return data;
        }
    }
}

public class tupleData
{
    public string responseCode { get; set; }
    public string message { get; set; }
}