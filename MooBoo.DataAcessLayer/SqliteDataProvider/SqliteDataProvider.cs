using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public class SqliteDataProvider : Singleton<SqliteDataProvider>, IRepository<LogItem>
    {
        public bool Create(LogItem obj)
        {
            throw new System.NotImplementedException();
        }

        public LogItem Read(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(object id, LogItem obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}
