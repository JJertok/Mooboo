using System.Collections.Generic;

namespace MooBoo.Infrastructure.Interfaces.DataLayer
{
    public interface IRepository<T> where T : DataObject
    {
        T Create(T obj);
        T Read(int id);
        IEnumerable<T> ReadAll();
        bool Update(T obj);
        bool Delete(int id);

    }
}
