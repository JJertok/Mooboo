using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer.SqliteDataProvider
{
    public class SqliteCategoryProvider : Singleton<SqliteCategoryProvider>, IRepository<Category>
    {
        public bool Create(Category obj)
        {
            throw new System.NotImplementedException();
        }

        public Category Read(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(object id, Category obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}
