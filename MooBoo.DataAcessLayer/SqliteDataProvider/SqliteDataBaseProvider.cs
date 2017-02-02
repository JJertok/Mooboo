using System;
using System.Data.SQLite;
using System.IO;
using MooBoo.Model.DataLayer;

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

                if (!IsTableExists(Category.Token))
                {
                    ExecuteQuery("CREATE TABLE CATEGORY (" +
                                  "ID INTEGER PRIMARY KEY," +
                                  "NAME VARCHAR(20)," +
                                  "VALUE DOUBLE" +
                                  ")");
                }
                if (!IsTableExists(Application.Token))
                {
                    ExecuteQuery("CREATE TABLE APPLICATION (" +
                                  "ID INTEGER PRIMARY KEY," +
                                  "FILENAME VARCHAR(100)," +
                                  "CATEGORYID INT" +
                                  ")");
                }
                if (!IsTableExists(LogItem.Token))
                {
                    ExecuteQuery("CREATE TABLE LOGITEM (" +
                                  "ID INTEGER PRIMARY KEY," +
                                  "START DATETIME," +
                                  "STOP DATETIME," +
                                  "APPLICATIONID INT" +
                                  ")");
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
            var connection = new SQLiteConnection(string.Format("DataSource={0};Version=3", DataBaseName));
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

        public static int GetLastTableId(string tableName)
        {
            try
            {
                var format = string.Format("SELECT * FROM {0} WHERE ID = (SELECT MAX(ID)  FROM {0});", tableName);
                using (var conn = GetConnection())
                using (var command = new SQLiteCommand(format, conn))
                {
                    var s = command.ExecuteScalar();
                    var str = s.ToString();
                    return int.Parse(str);
                }

            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static void Insert(string table, params object[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] is string)
                {
                    values[i] = string.Format("'{0}'", values[i]);
                }
            }
            var format = string.Format("INSERT INTO {0} VALUES({1})", table, string.Join(",", values));
            using (var c = GetConnection())
            using (var com = new SQLiteCommand(format, c))
            {
                com.ExecuteNonQuery();
                c.Close();
            }
        }

        public static void ExecuteQuery(string str)
        {
            using (var connection = SqliteDataBaseProvider.GetConnection())
            using (var command = new SQLiteCommand(str, connection))
            {
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static bool DeleteByIdFromTable(string table, int id)
        {
            try
            {
                var format = string.Format("DELETE FROM {0} WHERE ID= {1}", Category.Token, id);
                ExecuteQuery(format);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        
    }
}
