namespace MooBoo.Infrastructure.Interfaces.DataLayer
{
    public interface IRepository<T> where T : DataObject
    {
        bool Create(T obj);
        T Read(object id);
        bool Update(object id, T obj);
        bool Delete(object id);

    }
}
