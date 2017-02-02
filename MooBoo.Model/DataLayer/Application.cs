using MooBoo.Infrastructure.Interfaces.DataLayer;

namespace MooBoo.Model.DataLayer
{
    public class Application : DataObject
    {
        public static string Token = "APPLICATION";

        public int CategoryId { get; set; } = -1;
        public Category Category { get; set; }
        public string FileName { get; set; }

        public override DataObject Clone()
        {
            return new Application
            {
                Id = Id,
                Category = null,
                CategoryId = CategoryId,
                FileName = FileName
            };
        }
    }
}
