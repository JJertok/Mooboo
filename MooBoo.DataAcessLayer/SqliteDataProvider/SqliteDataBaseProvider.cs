using System;
using System.Data.SQLite;
using System.IO;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public static class SqliteDataBaseProvider
    {
        private static string DataBaseName = "MooBooSqlite.db";
        
        static SqliteDataBaseProvider()
        {
            var dbPath = Path.Combine(DataBaseName); //TODO maybe should be in different place
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(DataBaseName);
            }

            using (var connection = GetConnection())
            {

                if (!IsTableExists("CATEGORY"))
                {
                    new SQLiteCommand("CREATE TABLE CATEGORY (" +
                                  "NAME VARCHAR(20)," +
                                  "VALUE DOUBLE" +
                                  ")", connection).ExecuteNonQuery();
                }
                if (!IsTableExists("LOGITEM"))
                {
                    new SQLiteCommand("CREATE TABLE LOGITEM (" +
                                  "FILENAME VARCHAR(100)," +
                                  "START DATETIME," +
                                  "STOP DATETIME," +
                                  "CATEGORYID INT" +
                                  ")", connection).ExecuteNonQuery();
                }

                
                connection.Close();
            }
        }


        /// <summary>
        /// Each time you uses connection you should close it.
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(string.Format("DataSource={0};Version=3",DataBaseName));
            connection.Open();
            return connection;
        }


        private static bool IsTableExists(string tableName)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand(string.Format("SELECT * FROM sqlite_master WHERE type='table' AND name='{0}';", tableName), connection))
            {
                return command.ExecuteScalar() != null;
            }
        }
    }
}
