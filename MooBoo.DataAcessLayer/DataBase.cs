using System.Collections.Generic;
using Mooboo.Utilities;
using MooBoo.Model.DataLayer;
namespace MooBoo.DataAcessLayer
{
    public class DataBase : Singleton<DataBase>
    {
        public List<Category> Categories = new List<Category>();

        public List<LogItem> Logs = new List<LogItem>();
    }
}
