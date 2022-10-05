using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TEPSClientInstallService_Master.Classes
{
    public class sqlServerInteraction
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

                        checkForCatalog("GetInstalledCatalogs", exec);
                    }
                    return value;
                }

                checkForCatalog("GetInstalledCatalogs", exec);

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
                catalogByIDTable = executeReturningStoredProcedure(storedProcedureName, exec);

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
                        prm.Add(new SqlParameter("@Client_ID", SqlDbType.Int) { Value = executionText[0] });
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
                throw ex;
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
                throw;
            }
        }

        #endregion retrieving SQL Data
    }
}