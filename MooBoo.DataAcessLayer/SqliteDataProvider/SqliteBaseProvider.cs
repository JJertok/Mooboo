using System.Data.SQLite;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public abstract class SqliteBaseProvider
    {
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection();
        }
    }
}
