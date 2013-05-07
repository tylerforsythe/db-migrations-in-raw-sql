using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace DB_Migrations_in_Raw_SQL
{
    public class DatabaseVersion : IDisposable
    {
        SqlConnection dbConnection = null;


        public void ConnectToDatabase(string connectionString) {
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
        }

        public string ExecuteScriptFileAtPath(string scriptPath) {
            EnsureDbConnection();
            StringBuilder resultString = new StringBuilder(500);
            //wrap each script in a transaction.
            //SqlTransaction transaction = dbConnection.BeginTransaction();

            string[] fileLines = File.ReadAllLines(scriptPath);
            StringBuilder sb = new StringBuilder(2000);

            for (int i=0; i < fileLines.Length; ++i) {
                string line = fileLines[i];
                line = ReplaceTokens(line);
                if (line.ToUpper().Trim() == "GO" || i == fileLines.Length - 1) {
                    try {
                        if (i == fileLines.Length - 1)
                            sb.AppendLine(line);
                        if (sb.ToString().Trim().Length == 0) {
                            sb.Clear();
                            continue;
                        }
                        SqlCommand command = dbConnection.CreateCommand();
                        command.CommandTimeout = 20 * 60;
                        //command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = sb.ToString() + Environment.NewLine + Environment.NewLine;
                        int resultCount = command.ExecuteNonQuery();
                        resultString.AppendLine(string.Format("{0} rows affected.", resultCount));
                    }
                    catch (Exception e) {
                        //transaction.Rollback();
                        Exception ex = new Exception(sb.ToString() + " ----- " + e.Message);
                        throw ex;
                    }
                    sb.Clear();
                }
                else {
                    sb.AppendLine(line);
                }
            }

            SaveExecutionToDBVersionTable(scriptPath);
            //transaction.Commit();

            return resultString.ToString();
        }

        private string ReplaceTokens(string line) {
            string pathOfExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return line.Replace("$(EXE_RELATIVEPATH)", pathOfExe);
        }

        private void SaveExecutionToDBVersionTable(string scriptPath) {
            SqlCommand versionCommand = dbConnection.CreateCommand();
            //versionCommand.Transaction = transaction;
            versionCommand.CommandType = System.Data.CommandType.Text;
            versionCommand.CommandText = string.Format(
                "INSERT INTO DatabaseVersion (ScriptName, RunTimestamp) VALUES ('{0}', {1})",
                Path.GetFileName(scriptPath), "GETDATE()");
            int versionRows = versionCommand.ExecuteNonQuery();
            if (versionRows != 1) {
                //transaction.Rollback();
                throw new Exception("DatabaseVersion INSERT rows was not 1.");
            }
        }

        public bool DoesTableExist() {
            EnsureDbConnection();

            string tableExistQuery = Resources.ReadResource("DoesTableExist.sql");
            SqlCommand command = dbConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = tableExistQuery;
            object rawResult = command.ExecuteScalar();

            if (rawResult == null)
                return false;

            int result = (int)rawResult;
            return result > 0;
        }

        public void CreateTable() {
            EnsureDbConnection();

            string tableCreateQuery = Resources.ReadResource("CreateTable.sql");
            SqlCommand command = dbConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = tableCreateQuery;
            command.ExecuteNonQuery();

            if (!DoesTableExist())
                throw new Exception("Table did not create successfully.");
        }

        public List<string> WhichScriptsHaveNotBeenRun(List<string> fullListOfPotentials) {
            List<string> needToRun = new List<string>();
            //read DatabaseVersion table entries so we can see what scripts have been run

            foreach (string scriptName in fullListOfPotentials) {
                string tableExistQuery = Resources.ReadResource("HasScriptBeenRun.sql");
                SqlCommand command = dbConnection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = string.Format(tableExistQuery, Path.GetFileName(scriptName));
                object rawResult = command.ExecuteScalar();
                if (rawResult == null || (int)rawResult != 1) {
                    needToRun.Add(scriptName);
                }
            }

            return needToRun;
        }


        private void EnsureDbConnection() {
            if (dbConnection == null)
                throw new Exception("dbConnection is null");
            if (dbConnection.State != System.Data.ConnectionState.Open)
                throw new Exception("dbConnection is not open: " + dbConnection.State.ToString());
        }

        #region IDisposable Members

        public void Dispose() {
            if (dbConnection != null) {
                try {
                    dbConnection.Close();
                }
                catch (Exception e) {
                    throw e;
                }
            }
        }

        #endregion
    }
}
