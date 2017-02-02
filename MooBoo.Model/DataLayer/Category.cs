using MooBoo.Infrastructure.Interfaces.DataLayer;

namespace MooBoo.Model.DataLayer
{
    public class Category : DataObject
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public override DataObject Clone()
        {
            return new Category
            {
                Name = Name,
                Value = Value,
                Id = Id
            };
        }

        public static string Token = "CATEGORY";
    }
}
