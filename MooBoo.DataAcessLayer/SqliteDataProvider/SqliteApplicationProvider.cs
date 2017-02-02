using System;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    class SqliteApplicationProvider : Singleton<SqliteApplicationProvider>, IRepository<Application>
    {
        public Application Create(Application obj)
        {
            throw new NotImplementedException();
        }

        public Application Read(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Application obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            return SqliteDataBaseProvider.DeleteByIdFromTable(Application.Token, id);
        }
    }
}
