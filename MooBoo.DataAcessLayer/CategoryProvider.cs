using System.Linq;
using Mooboo.Utilities;
using MooBoo.Infrastructure.Interfaces.DataLayer;
using MooBoo.Model.DataLayer;

namespace MooBoo.DataAcessLayer
{
    public class CategoryProvider : Singleton<CategoryProvider>, ICategoryProvider
    {
        public bool Create(Category obj)
        {
            var c = DataBase.Instance.Categories.FirstOrDefault(x => x.Id == obj.Id);
            if (c != null)
            {
                return false;
            }
            DataBase.Instance.Categories.Add(new Category
            {
                Id = DataBase.Instance.Categories.Max(m => m.Id) + 1,
                Value = obj.Value,
                Name = obj.Name,
            });
            return true;
        }

        public Category Read(object id)
        {
            var c = DataBase.Instance.Categories.FirstOrDefault(x => x.Id == (int) id);
            if (c == null)
            {
                return null;
            }

            return new Category
            {
                Id = c.Id,
                Value = c.Value,
                Name = c.Name,
            };
        }

        public bool Delete(object id)
        {
            var c = DataBase.Instance.Categories.FirstOrDefault(x => x.Id == (int)id);
            if (c == null)
            {
                return false;
            }
            DataBase.Instance.Categories.Remove(c);
            return true;
        }

        public bool Update(object id, Category obj)
        {
            var c = DataBase.Instance.Categories.FirstOrDefault(x => x.Id == (int)id);
            if (c == null)
            {
                return false;
            }
            c.Name = obj.Name;
            c.Value = obj.Value;
            return true;
        }
    }
}
