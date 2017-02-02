using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public class SqliteCategoryProvider : Singleton<SqliteCategoryProvider>, IRepository<Category>
    {
        public Category Create(Category obj)
        {
            try
            {
                var lastId = SqliteDataBaseProvider.GetLastTableId(Category.Token);
                SqliteDataBaseProvider.Insert(Category.Token, lastId + 1, obj.Name, obj.Value);
                return new Category
                {
                    Id = lastId + 1,
                    Name = obj.Name,
                    Value = obj.Value
                };
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public Category Read(int id)
        {
            try
            {
                var format = string.Format("SELECT * FROM {0} WHERE ID='{1}'",
                    Category.Token,
                    id);
                using (var connection = SqliteDataBaseProvider.GetConnection())
                using (var command = new SQLiteCommand(format, connection))
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    var ide = int.Parse(reader["ID"].ToString());
                    var name = reader["NAME"].ToString();
                    var value = double.Parse(reader["VALUE"].ToString());
                    return new Category
                    {
                        Id = ide,
                        Name = name,
                        Value = value
                    };
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Category> ReadAll()
        {
            var result = new List<Category>();
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
                        var name = reader["NAME"].ToString();
                        var value = double.Parse(reader["VALUE"].ToString());
                        result.Add(new Category
                        {
                            Id = ide,
                            Name = name,
                            Value = value
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

        public bool Update(Category obj)
        {
            try
            {
                var format = string.Format("UPDATE {0} SET NAME ='{1}',VALUE={2} WHERE ID={3}",
                    Category.Token,
                    obj.Name,
                    obj.Value,
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
            return SqliteDataBaseProvider.DeleteByIdFromTable(Category.Token, id);
        }
    }
}
