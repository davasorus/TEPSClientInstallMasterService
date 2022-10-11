using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TEPSClientInstallService_Master.Classes
{
    public class sqlServerInteractionClass
    {
        private loggingClass loggingClass = new loggingClass();

        #region returning sql data

        public string returnClientName(string storedProcedureName, string[] executionText)
        {
            string[] exec = { executionText[0] };

            DataTable clientByIDTable = new DataTable();

            string value = "";

            try
            {
                clientByIDTable = executeReturningStoredProcedure(storedProcedureName, exec);

                if (clientByIDTable.Rows.Count > 0)
                {
                    foreach (DataRow row in clientByIDTable.Rows)
                    {
                        value = row[1].ToString();

                        checkForCatalog("GetInstalledCatalogByID", exec);
                    }
                    return value;
                }

                checkForCatalog("GetInstalledCatalogByID", exec);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool checkForCatalog(string storedProcedureName, string[] executionText)
        {
            string[] exec = { executionText[0] };

            DataTable catalogByIDTable = new DataTable();

            bool value = false;

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
                        prm.Add(new SqlParameter("@PreReq_Name", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@PreReq_Path", SqlDbType.NVarChar) { Value = executionText[1] });
                        break;

                    case "InsertNewCatalog":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = executionText[0] });
                        break;

                    case "InsertNewClient":
                        prm.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar) { Value = executionText[0] });
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

                    case "GetClientByID":
                        prm.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = executionText[0] });
                        cmd.Parameters.AddRange(prm.ToArray());

                        using (da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }

                        break;

                    case "GetInstalledCatalogByID":
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = executionText[0] });
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