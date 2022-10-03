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
                    }
                    return value;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
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
                    case "CreateProfile":
                        prm.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@SourceLocation", SqlDbType.NVarChar) { Value = executionText[1] });
                        prm.Add(new SqlParameter("@DestinationLocation", SqlDbType.NVarChar) { Value = executionText[2] });
                        prm.Add(new SqlParameter("@LastCopyType", SqlDbType.NVarChar) { Value = executionText[3] });
                        break;

                    case "UpdateProfile":
                        prm.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = executionText[0] });
                        prm.Add(new SqlParameter("@SourceLocation", SqlDbType.NVarChar) { Value = executionText[1] });
                        prm.Add(new SqlParameter("@DestinationLocation", SqlDbType.NVarChar) { Value = executionText[2] });
                        prm.Add(new SqlParameter("@LastCopyType", SqlDbType.NVarChar) { Value = executionText[3] });
                        prm.Add(new SqlParameter("@LastCopyDuration", SqlDbType.NVarChar) { Value = executionText[4] });
                        break;

                    case "DeleteProfile":
                        prm.Add(new SqlParameter("@name", SqlDbType.NVarChar) { Value = executionText[0] });
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

                    case "GetInstalledCatalog":
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