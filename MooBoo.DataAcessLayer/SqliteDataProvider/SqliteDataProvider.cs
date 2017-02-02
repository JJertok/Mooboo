using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public class SqliteDataProvider : Singleton<SqliteDataProvider>, IRepository<LogItem>
    {
        public LogItem Create(LogItem obj)
        {
            try
            {
                var lastId = SqliteDataBaseProvider.GetLastTableId(LogItem.Token);
                SqliteDataBaseProvider.Insert(LogItem.Token, lastId + 1, obj.Start, obj.Stop, obj.ApplicationId);
                return new LogItem
                {
                    Id = lastId + 1,
                    ApplicationId = obj.ApplicationId,
                    Application = obj.Application,
                    Stop = obj.Stop,
                    Start = obj.Start
                };
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public LogItem Read(int id)
        {
            try
            {
                var format = string.Format("SELECT * FROM {0} WHERE ID='{1}'",
                    LogItem.Token,
                    id);
                using (var connection = SqliteDataBaseProvider.GetConnection())
                using (var command = new SQLiteCommand(format, connection))
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    var ide = int.Parse(reader["ID"].ToString());
                    var start = DateTime.Parse(reader["START"].ToString());
                    var stop = DateTime.Parse(reader["STOP"].ToString());
                    var appid = int.Parse(reader["APPLICATIONID"].ToString());
                    return new LogItem
                    {
                        Id = ide,
                        Start = start,
                        Stop = stop,
                        ApplicationId = appid,
                        Application = SqliteApplicationProvider.Instance.Read(appid)
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<LogItem> ReadAll()
        {
            var result = new List<LogItem>();
            try
            {
                var format = string.Format("SELECT * FROM {0} ",
                    LogItem.Token);
                using (var connection = SqliteDataBaseProvider.GetConnection())
                using (var command = new SQLiteCommand(format, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ide = int.Parse(reader["ID"].ToString());
                        var start = DateTime.Parse(reader["START"].ToString());
                        var stop = DateTime.Parse(reader["STOP"].ToString());
                        var appid = int.Parse(reader["APPLICATIONID"].ToString());
                        result.Add(new LogItem
                        {
                            Id = ide,
                            Start = start,
                            Stop = stop,
                            ApplicationId = appid,
                            Application = SqliteApplicationProvider.Instance.Read(appid)
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(LogItem obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            return SqliteDataBaseProvider.DeleteByIdFromTable(LogItem.Token, id);

        }
    }
}
