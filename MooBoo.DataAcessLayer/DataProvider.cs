using System.Linq;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer
{
    public class DataProvider : Singleton<DataProvider>, IDataProvider
    {
        
        public bool Create(LogItem obj)
        {
            var c = DataBase.Instance.Logs.FirstOrDefault(x => x.Id == obj.Id);
            if (c != null)
            {
                return false;
            }
            DataBase.Instance.Logs.Add(new LogItem
            {
                Id = DataBase.Instance.Logs.Max(m => m.Id) + 1,
                CategoryId = obj.CategoryId,
                FileName = obj.FileName,
                Start = obj.Start,
                Stop = obj.Stop
            });
            return true;
        }

        public LogItem Read(object id)
        {
            var c = DataBase.Instance.Logs.FirstOrDefault(x => x.Id == (int)id);
            if (c == null)
            {
                return null;
            }

            return new LogItem
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                FileName = c.FileName,
                Start = c.Start,
                Stop = c.Stop
            };
        }

        public bool Update(object id, LogItem obj)
        {
            var c = DataBase.Instance.Logs.FirstOrDefault(x => x.Id == (int)id);
            if (c == null)
            {
                return false;
            }
            DataBase.Instance.Logs.Remove(c);
            return true;
        }

        public bool Delete(object id)
        {
            var c = DataBase.Instance.Logs.FirstOrDefault(x => x.Id == (int)id);
            if (c == null)
            {
                return false;
            }
            c.CategoryId = c.CategoryId;
            c.FileName = c.FileName;
            c.Start = c.Start;
            c.Stop = c.Stop;
            return true;
        }
    }
}
