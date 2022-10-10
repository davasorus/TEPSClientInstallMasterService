using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
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
            dynamic jsonObj = JsonConvert.DeserializeObject(body);

            return jsonObj["Instance"];
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

                    case string t when message.Contains("MSP"):
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
    }
}

public class tupleData
{
    public string responseCode { get; set; }
    public string message { get; set; }
}