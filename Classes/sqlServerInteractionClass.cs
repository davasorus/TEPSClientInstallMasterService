using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    public class sqlServerInteractionClass
    {
        private loggingClass loggingClass = new loggingClass();

        #region returning sql data

        public string returnClientName(string storedProcedureName, string[] executionText)
        {
            try
            {
                string[] exec = { executionText[0] };

                DataTable clientByIDTable = new DataTable();

                string value = "";

                clientByIDTable = executeReturningStoredProcedure(storedProcedureName, exec);

                if (clientByIDTable.Rows.Count > 0)
                {
                    foreach (DataRow row in clientByIDTable.Rows)
                    {
                        value = row[1].ToString();

                        checkForCatalog("GetInstalledCatalogByID", exec);

                        try
                        {
                            if (executionText[1].Length > 0)
                            {
                                executeNonReturningStoredProcedure("UpdateClientInstance", executionText);
                            }
                        }
                        catch (Exception ex)
                        {
                            loggingClass.logEntryWriter(ex.ToString(), "error");
                        }
                    }
                    return value;
                }

                checkForCatalog("GetInstalledCatalogByID", exec);
                executeNonReturningStoredProcedure("UpdateClientInstance", executionText);

                return null;
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                return null;
            }
        }

        public bool checkForCatalog(string storedProcedureName, string[] executionText)
        {
            string[] exec = { executionText[0] };

            DataTable catalogByIDTable = new DataTable();

            try
            {
                //catalogByIDTable = executeReturningStoredProcedure(storedProcedureName, exec);
                catalogByIDTable = executeReturningStoredProcedure("GetInstalledCatalogByID", exec);

                if (catalogByIDTable.Rows.Count > 0)
                {
                    return true;
                }

                executeNonReturningStoredProcedure("InsertNewCatalog", exec);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void checkForClient(string storedProcedureName, string[] executionText)
        {
            string[] exec = { executionText[0] };
            List<string> exec1 = new List<string>();

            DataTable catalogByIDTable = new DataTable();
            DataTable clientByNameTable = new DataTable();

            //bool value = false;

            try
            {
                clientByNameTable = executeReturningStoredProcedure(storedProcedureName, exec);

                if (clientByNameTable.Rows.Count > 0)
                {
                    foreach (DataRow row in clientByNameTable.Rows)
                    {
                        exec1.Add(row[0].ToString());
                    }
                }
                else
                {
                    executeNonReturningStoredProcedure("InsertNewClient", executionText);
                }

                catalogByIDTable = executeReturningStoredProcedure("GetInstalledCatalogByID", exec1.ToArray());

                if (catalogByIDTable.Rows.Count > 0)
                {
                }
                else
                {
                    executeNonReturningStoredProcedure("InsertNewCatalog", exec1.ToArray());
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        public void checkForSettings(string storedProcedurename, string[] executionText)
        {
            string[] exec = { executionText[6] };
            DataTable settingByInstance = new DataTable();

            settingByInstance = executeReturningStoredProcedure("GetSettingByInstance", exec);

            if (settingByInstance.Rows.Count > 0)
            {
                executeNonReturningStoredProcedure("UpdateSettingCADServerName", executionText);
                executeNonReturningStoredProcedure("UpdateSettingESSServerName", executionText);
                executeNonReturningStoredProcedure("UpdateSettingGISInstance", executionText);
                executeNonReturningStoredProcedure("UpdateSettingGISServerName", executionText);
                executeNonReturningStoredProcedure("UpdateSettingRecordsServerName", executionText);
                executeNonReturningStoredProcedure("UpdateSettingMobileServerName", executionText);
                executeNonReturningStoredProcedure("UpdateSettingClientInstallPath", executionText);
            }
            else
            {
                loggingClass.logEntryWriter("this should not have happened", "error");
            }
        }

        public DataTable returnSettingsDBValue(string[] exec)
        {
            try
            {
                DataTable settingDBValues = new DataTable();

                settingDBValues = executeReturningStoredProcedure("GetSettingByInstance", exec);

                if (settingDBValues.Rows.Count > 0)
                {
                    return settingDBValues;
                }

                return settingDBValues;
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
                return null;
            }
        }

        public async Task checkForPreReq(string storedProcedureName, string[] executionText)
        {
            try
            {
                string[] exec = { executionText[0], executionText[1] };

                DataTable preReqByNameTable = new DataTable();

                preReqByNameTable = executeReturningStoredProcedure(storedProcedureName, exec);

                if (preReqByNameTable.Rows.Count > 0)
                {
                    foreach (DataRow row in preReqByNameTable.Rows)
                    {
                    }
                }
                else
                {
                    executeNonReturningStoredProcedure("InsertPreReq", executionText);
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        public async Task checkForAgency(string storedProcedureName, string[] executionText)
        {
            try
            {
                string[] exec = { executionText[0], executionText[1] };

                DataTable agencyByNameTable = new DataTable();

                agencyByNameTable = executeReturningStoredProcedure(storedProcedureName, exec);

                if (agencyByNameTable.Rows.Count > 0)
                {
                    foreach (DataRow row in agencyByNameTable.Rows)
                    {
                    }
                }
                else
                {
                    if (storedProcedureName.Contains("ORI"))
                    {
                        executeNonReturningStoredProcedure("InsertNewORI", executionText);
                    }
                    else
                    {
                        executeNonReturningStoredProcedure("InsertNewFDID", executionText);
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        public DataTable returnPreReqTable(string[] exec)
        {
            try
            {
                DataTable returnPreReqTable = new DataTable();

                returnPreReqTable = executeReturningStoredProcedure("GetPreReqByName", exec);

                if (returnPreReqTable.Rows.Count > 0)
                {
                    return returnPreReqTable;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
                return null;
            }
        }

        public DataTable returnClientTable(string[] exec)
        {
            DataTable returnClientTable = new DataTable();

            DataTable returnTable = new DataTable();

            try
            {
                if (exec.Count() > 0)
                {
                    returnClientTable = executeReturningStoredProcedure("GetClientsByEnrolledTypeID", exec);

                    if (returnClientTable.Rows.Count > 0)
                    {
                        returnTable = returnClientTable;

                        return returnTable;
                    }
                    else
                    {
                        returnTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                returnClientTable = executeReturningStoredProcedure("GetClients", exec);

                if (returnClientTable.Rows.Count > 0)
                {
                    returnTable = returnClientTable;

                    return returnTable;
                }
                else
                {
                    returnTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        public DataTable returnCatalogTable(string[] exec)
        {
            DataTable catalogTable = new DataTable();
            DataTable returnTable = new DataTable();

            try
            {
                if (exec.Count() > 0)
                {
                    catalogTable = executeReturningStoredProcedure("GetInstalledCatalogByID", exec);
                    if (catalogTable.Rows.Count > 0)
                    {
                        returnTable = catalogTable;
                        return returnTable;
                    }
                    else
                    {
                        returnTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                catalogTable = executeReturningStoredProcedure("GetInstalledCatalogs", exec);

                if (catalogTable.Rows.Count > 0)
                {
                    returnTable = catalogTable;
                    return returnTable;
                }
                else
                {
                    returnTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        public DataTable returnErrorTable(string storedProcedureName)
        {
            DataTable errorTable = new DataTable();
            DataTable returnTable = new DataTable();
            string[] exec = { };

            try
            {
                if (storedProcedureName.Equals("GetTop50Errors"))
                {
                    errorTable = executeReturningStoredProcedure("GetTop50Errors", exec);
                    if (errorTable.Rows.Count > 0)
                    {
                        returnTable = errorTable;
                        return returnTable;
                    }
                    else
                    {
                        errorTable = null;
                        return returnTable;
                    }
                }
                else if (storedProcedureName.Equals("GetTop1000Errors"))
                {
                    errorTable = executeReturningStoredProcedure("GetTop1000Errors", exec);
                    if (errorTable.Rows.Count > 0)
                    {
                        returnTable = errorTable;
                        return returnTable;
                    }
                    else
                    {
                        errorTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
                return returnTable;
            }

            return returnTable;
        }

        public DataTable returnInstallHistory(string[] exec)
        {
            DataTable historyTable = new DataTable();
            DataTable returnTable = new DataTable();

            try
            {
                if (exec[0] != null)
                {
                    historyTable = executeReturningStoredProcedure("GetInstallHistoryByEnrolledType", exec);

                    if (historyTable.Rows.Count > 0)
                    {
                        returnTable = historyTable;
                        return returnTable;
                    }
                    else
                    {
                        historyTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                historyTable = executeReturningStoredProcedure("GetInstallHistory", exec);

                if (historyTable.Rows.Count > 0)
                {
                    returnTable = historyTable;
                    return returnTable;
                }
                else
                {
                    historyTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        public DataTable returnUnInstallHistory(string[] exec)
        {
            DataTable historyTable = new DataTable();
            DataTable returnTable = new DataTable();

            try
            {
                if (exec[0] != null)
                {
                    historyTable = executeReturningStoredProcedure("GetUnInstallHistoryByEnrolledType", exec);

                    if (historyTable.Rows.Count > 0)
                    {
                        returnTable = historyTable;
                        return returnTable;
                    }
                    else
                    {
                        historyTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                historyTable = executeReturningStoredProcedure("GetUnInstallHistory", exec);

                if (historyTable.Rows.Count > 0)
                {
                    returnTable = historyTable;
                    return returnTable;
                }
                else
                {
                    historyTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        public DataTable returnORIs(string[] exec)
        {
            DataTable oriTable = new DataTable();
            DataTable returnTable = new DataTable();

            try
            {
                if (exec[0] != null)
                {
                    oriTable = executeReturningStoredProcedure("GetORIByNameByEnrolledInstanceType", exec);

                    if (oriTable.Rows.Count > 0)
                    {
                        returnTable = oriTable;
                        return returnTable;
                    }
                    else
                    {
                        oriTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                oriTable = executeReturningStoredProcedure("GetAllORIs", exec);

                if (oriTable.Rows.Count > 0)
                {
                    returnTable = oriTable;
                    return returnTable;
                }
                else
                {
                    oriTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        public DataTable returnFDIDs(string[] exec)
        {
            DataTable fdidTable = new DataTable();
            DataTable returnTable = new DataTable();

            try
            {
                if (exec[0] != null)
                {
                    fdidTable = executeReturningStoredProcedure("GetFDIDByNameByEnrolledInstanceType", exec);

                    if (fdidTable.Rows.Count > 0)
                    {
                        returnTable = fdidTable;
                        return returnTable;
                    }
                    else
                    {
                        fdidTable = null;
                        return returnTable;
                    }
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            try
            {
                fdidTable = executeReturningStoredProcedure("GetAllFDIDs", exec);

                if (fdidTable.Rows.Count > 0)
                {
                    returnTable = fdidTable;
                    return returnTable;
                }
                else
                {
                    fdidTable = null;
                    return returnTable;
                }
            }
            catch (Exception ex)
            {
                returnTable = null;
            }

            return returnTable;
        }

        #endregion returning sql data

        #region retrieving SQL Data

        public void executeNonReturningStoredProcedure(string storedProcedureName, string[] executionText)
        {
            string connectionstring = "";
            SqlDataReader rdr = null;
            SqlConnection cnn;
            List<SqlParameter> prm = new List<SqlParameter>();

            connectionstring = $"Data Source ={configValues.DBName};Initial Catalog=TylerClientIMS;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionstring);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(storedProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                //switch should be the name in DB for the SP
                switch (storedProcedureName)
                {
                    case "InsertInstallHistory":
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        prm.Add(new SqlParameter("@Action", SqlDbType.NVarChar) { Value = executionText[2] });
                        break;

                    case "InsertUninstallHistory":
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        prm.Add(new SqlParameter("@Action", SqlDbType.NVarChar) { Value = executionText[2] });
                        break;

                    case "InsertErrorLog":
                        prm.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[1] });
                        break;

                    case "InsertPreReq":
                        prm.Add(new SqlParameter("@EnrolledInstanceType", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@PreReq_Name", SqlDbType.NVarChar) { Value = executionText[1] });
                        prm.Add(new SqlParameter("@PreReq_Path", SqlDbType.NVarChar) { Value = executionText[2] });
                        break;

                    case "InsertNewCatalog":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = executionText[0] });
                        break;

                    case "InsertNewClient":
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[0] });
                        break;

                    case "InsertNewORI":
                        prm.Add(new SqlParameter("@ORI", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        break;

                    case "InsertNewFDID":
                        prm.Add(new SqlParameter("@FDID", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLComp3532":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCompact3532_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLComp3564":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCompact3564_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLComp0464":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCompact0464_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLCLR200832":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCLR200832_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLCLR200864":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCLR200864_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogScenePD":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@ScenePD_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogUpdater":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@Updater_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogGISComp32":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@GISComponents32_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogGISComp64":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@GISComponents64_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogDotNet":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@DotNet_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLCLR201232":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCLR201232_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogSQLCLR201264":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@SQLCLR201264_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogDBProvider":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@DBProvider_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogLERMS":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@LERMS_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogCAD":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@CAD_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogCADObserver":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@CADObserver_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });
                        break;

                    case "UpdateCatalogFireMobile":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@FireMobile_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });

                        break;

                    case "UpdateCatalogLEMobile":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@LEMobile_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });

                        break;

                    case "UpdateCatalogMergeMobile":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@MobileMerge_installed", SqlDbType.Bit) { Value = int.Parse(executionText[1]) });

                        break;

                    case "UpdateCatalogMobileConfig":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@MobileAgencyConfig", SqlDbType.NVarChar) { Value = executionText[1] });

                        break;

                    case "UpdateSettingESSServerName":
                        prm.Add(new SqlParameter("@EssServerName", SqlDbType.NVarChar) { Value = executionText[1] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateSettingRecordsServerName":
                        prm.Add(new SqlParameter("@RecordsServerName", SqlDbType.NVarChar) { Value = executionText[2] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateSettingCADServerName":
                        prm.Add(new SqlParameter("@CADServerName", SqlDbType.NVarChar) { Value = executionText[3] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateSettingGISServerName":
                        prm.Add(new SqlParameter("@GISServerName", SqlDbType.NVarChar) { Value = executionText[4] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateSettingGISInstance":
                        prm.Add(new SqlParameter("@GISInstance", SqlDbType.NVarChar) { Value = executionText[5] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateSettingMobileServerName":
                        prm.Add(new SqlParameter("@MobileServername", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    case "UpdateClientInstance":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        prm.Add(new SqlParameter("@client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });

                        break;

                    case "UpdateSettingClientInstallPath":
                        prm.Add(new SqlParameter("@ClientInstallPath", SqlDbType.NVarChar) { Value = executionText[7] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[6]) });
                        break;

                    default:
                        break;
                }

                cmd.Parameters.AddRange(prm.ToArray());

                rdr = cmd.ExecuteReader();

                rdr.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
            }
        }

        public DataTable executeReturningStoredProcedure(string storedProcedureName, string[] executionText)
        {
            DataTable result = new DataTable();
            string connectionstring;
            SqlConnection cnn;
            SqlDataAdapter da = null;
            List<SqlParameter> prm = new List<SqlParameter>();

            connectionstring = $"Data Source ={configValues.DBName};Initial Catalog=TylerClientIMS;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionstring);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand(storedProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                switch (storedProcedureName)
                {
                    case "GetClientMachineProfiles":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetInstalledCatalogs":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetTop50Errors":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetTop1000Errors":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetClientByID":
                        prm.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());

                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetInstalledCatalogByID":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                        break;

                    case "GetClientByName":
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[0] });
                        cmd.Parameters.AddRange(prm.ToArray());

                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetSettingByInstance":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                        break;

                    case "GetPreReqByName":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        prm.Add(new SqlParameter("@PreReqName", SqlDbType.NVarChar) { Value = executionText[1] });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                        break;

                    case "GetClients":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetClientsByEnrolledTypeID":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetInstallHistoryByEnrolledType":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetInstallHistory":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetUnInstallHistoryByEnrolledType":
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[0]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetUnInstallHistory":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetAllORIs":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetAllFDIDs":
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetORIByNameByEnrolledInstanceType":
                        prm.Add(new SqlParameter("@ORI", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetFDIDByNameByEnrolledInstanceType":
                        prm.Add(new SqlParameter("@FDID", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@EnrolledInstanceType_ID", SqlDbType.Int) { Value = int.Parse(executionText[1]) });
                        cmd.Parameters.AddRange(prm.ToArray());
                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    default:
                        break;
                }

                cnn.Close();

                return result;
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");
                throw ex;
            }
        }

        #endregion retrieving SQL Data
    }
}