using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mongoloid
{
    internal class SqlFunctions
    {
        /// <summary>
        /// Add a MongoDB instance to the SQL Server.
        /// </summary>
        /// <param name="records">A list of Shodan records to be added</param>
        /// <returns>Returns true if adding the Shodan records was successful</returns>
        public static bool AddShodanRecordToSqlDatabase(List<ShodanRecord> records)
        {
            using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
            {
                sqlConnection.Open();
                foreach (var record in records)
                {
                    if (CheckIfMongoInstanceExistsInSql(record.Ip, record.Port, sqlConnection))
                        continue;
                    using (var sqlCommand = new SqlCommand("AddShodanRecord", sqlConnection) { CommandType = CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("Ip", record.Ip));
                        sqlCommand.Parameters.Add(new SqlParameter("Port", record.Port));
                        sqlCommand.Parameters.Add(new SqlParameter("Banner", record.Banner));
                        sqlCommand.Parameters.Add(new SqlParameter("Timestamp", record.Timestamp));
                        sqlCommand.Parameters.Add(new SqlParameter("Hostnames", record.Hostnames));
                        sqlCommand.Parameters.Add(new SqlParameter("Country", record.Country));
                        sqlCommand.Parameters.Add(new SqlParameter("City", record.City));
                        sqlCommand.Parameters.Add(new SqlParameter("OperatingSystem", record.OperatingSystem));
                        sqlCommand.Parameters.Add(new SqlParameter("Organization", record.Organization));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check if a MongoDB instance has already been added to SQL Server.
        /// </summary>
        /// <param name="ip">MongoDB host IP</param>
        /// <param name="port">MongoDB port</param>
        /// <param name="sqlConnection">Existing SQL Connection to use</param>
        /// <returns>Returns true if a MongoDB instance already exists in the known hosts table</returns>
        public static bool CheckIfMongoInstanceExistsInSql(string ip, int port, SqlConnection sqlConnection)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
            using (var sqlCommand = new SqlCommand("CheckIfMongoInstanceExists", sqlConnection) { CommandType = CommandType.StoredProcedure })
            {
                sqlCommand.Parameters.Add(new SqlParameter("Ip", ip));
                sqlCommand.Parameters.Add(new SqlParameter("Port", port));
                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    sqlReader.Read();
                    return int.Parse(sqlReader[0].ToString()) > 0;
                }
            }
        }

        /// <summary>
        /// Get all known MongoDB ransom schemas from SQL
        /// </summary>
        /// <returns>Returns a list of known MongoDB ransom schemas</returns>
        public static List<RansomSchema> GetAllRansomSchemas()
        {
            var output = new List<RansomSchema>();
            using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetRansomSchemas", sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            output.Add(new RansomSchema(sqlReader["DatabaseName"].ToString(), sqlReader["CollectionName"].ToString(), sqlReader["BitcoinField"].ToString(), sqlReader["EmailField"].ToString(), sqlReader["NotesField"].ToString()) { ID = int.Parse(sqlReader["ID"].ToString()) });
                        }
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// Get all MongoDB hosts and their IPs from SQL Server
        /// </summary>
        /// <returns>Returns a list of RansomDemand objects with known MongoDB hosts populated</returns>
        public static List<RansomDemand> GetAllIpsAndPorts()
        {
            var output = new List<RansomDemand>();
            using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetAllIPs", sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                            output.Add(new RansomDemand { Ip = sqlReader["IP"].ToString(), Port = int.Parse(sqlReader["Port"].ToString()) });
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// Check if a ransom demand has already been added to the SQL Server
        /// </summary>
        /// <param name="ransomDemand">The populated Ransom Demand object to check</param>
        /// <param name="sqlConnection">An existing SQLConnection to use</param>
        /// <returns>Returns true if the ransom demand already exists in SQL Server</returns>
        public static bool CheckIfRansomDemandExists(RansomDemand ransomDemand, SqlConnection sqlConnection)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("CheckIfRansomDemandExists", sqlConnection) { CommandType = CommandType.StoredProcedure })
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IP", ransomDemand.Ip));
                    sqlCommand.Parameters.Add(new SqlParameter("Port", ransomDemand.Port));
                    sqlCommand.Parameters.Add(new SqlParameter("DatabaseName", ransomDemand.DatabaseName));
                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                            return int.Parse(sqlReader[0].ToString()) > 0;
                    }
                }
            }
            catch
            {
                //Do nothing, there was an error we can't recover from.
            }
            return false;
        }

        /// <summary>
        /// Add a list of ransom demands to SQL Server
        /// </summary>
        /// <param name="ransomDemands">A list of ransom demands to add</param>
        /// <returns>Returns true if adding the ransom demands was successful</returns>
        public static bool AddRansomDemands(List<RansomDemand> ransomDemands)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
                {
                    sqlConnection.Open();
                    foreach (var ransomDemand in ransomDemands)
                    {
                        if (CheckIfRansomDemandExists(ransomDemand, sqlConnection))
                            continue;
                        using (var sqlCommand = new SqlCommand("AddRansomDemand", sqlConnection) { CommandType = CommandType.StoredProcedure })
                        {
                            sqlCommand.Parameters.Add(new SqlParameter("IP", ransomDemand.Ip));
                            sqlCommand.Parameters.Add(new SqlParameter("Port", ransomDemand.Port));
                            sqlCommand.Parameters.Add(new SqlParameter("DatabaseName", ransomDemand.DatabaseName));
                            sqlCommand.Parameters.Add(new SqlParameter("Email", ransomDemand.Email ?? string.Empty));
                            sqlCommand.Parameters.Add(new SqlParameter("BitcoinWallet", ransomDemand.BitcoinWallet ?? string.Empty));
                            sqlCommand.Parameters.Add(new SqlParameter("RansomNote", ransomDemand.RansomText ?? string.Empty));
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a ransom schema from the SQL database.
        /// </summary>
        /// <param name="ransomSchema">The ransom schema object you want to delete</param>
        /// <returns></returns>
        public static bool DeleteRansomSchema(RansomSchema ransomSchema)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand("DeleteRansomSchema", sqlConnection) { CommandType = CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("ID", ransomSchema.ID));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new ransom schema to the SQL database or update an existing one.
        /// </summary>
        /// <param name="ransomSchema">The ransom schema object you want to add.</param>
        /// <returns></returns>
        public static bool AddUpdateRansomSchema(RansomSchema ransomSchema)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(Constants.SqlConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand("AddRansomSchema", sqlConnection) { CommandType = CommandType.StoredProcedure })
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("DatabaseName", ransomSchema.DatabaseName));
                        sqlCommand.Parameters.Add(new SqlParameter("CollectionName", ransomSchema.CollectionName));
                        sqlCommand.Parameters.Add(new SqlParameter("EmailField", ransomSchema.EmailField ?? "NONE"));
                        sqlCommand.Parameters.Add(new SqlParameter("BitcoinField", ransomSchema.BitcoinField ?? "NONE"));
                        sqlCommand.Parameters.Add(new SqlParameter("NotesField", ransomSchema.NotesField ?? "NONE"));
                        sqlCommand.Parameters.Add(new SqlParameter("ID", ransomSchema.ID));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
