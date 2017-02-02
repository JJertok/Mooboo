using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public class SqliteApplicationProvider : Singleton<SqliteApplicationProvider>, IRepository<Application>
    {
        public Application Create(Application obj)
        {
            try
            {
                var lastId = SqliteDataBaseProvider.GetLastTableId(Application.Token);
                SqliteDataBaseProvider.Insert(Application.Token, lastId + 1, obj.FileName, obj.CategoryId);
                return new Application
                {
                    Id = lastId + 1,
                    FileName = obj.FileName,
                    CategoryId = obj.CategoryId
                };
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public Application Read(int id)
        {
            try
            {
                var format = string.Format("SELECT * FROM {0} WHERE ID='{1}'",
                    Application.Token,
                    id);
                using (var connection = SqliteDataBaseProvider.GetConnection())
                using (var command = new SQLiteCommand(format, connection))
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    var ide = int.Parse(reader["ID"].ToString());
                    var name = reader["FILENAME"].ToString();
                    var value = int.Parse(reader["CATEGORYID"].ToString());
                    return new Application
                    {
                        Id = ide,
                        FileName = name,
                        CategoryId = value,
                        Category = SqliteCategoryProvider.Instance.Read(value)
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public IEnumerable<Application> ReadAll()
        {
            var result = new List<Application>();
            try
            {
                var format = string.Format("SELECT * FROM {0} ",
                    Application.Token);
                using (var connection = SqliteDataBaseProvider.GetConnection())
                using (var command = new SQLiteCommand(format, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ide = int.Parse(reader["ID"].ToString());
                        var name = reader["FILENAME"].ToString();
                        var value = int.Parse(reader["CATEGORYID"].ToString());
                        result.Add(new Application
                        {
                            Id = ide,
                            FileName = name,
                            CategoryId = value,
                            Category = SqliteCategoryProvider.Instance.Read(value)
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

        public bool Update(Application obj)
        {
            try
            {
                var format = string.Format("UPDATE {0} SET FILENAME ='{1}',CATEGORYID={2} WHERE ID={3}",
                    Application.Token,
                    obj.FileName,
                    obj.CategoryId,
                    obj.Id
                    );
                SqliteDataBaseProvider.ExecuteQuery(format);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            return SqliteDataBaseProvider.DeleteByIdFromTable(Application.Token, id);
        }
    }
}
