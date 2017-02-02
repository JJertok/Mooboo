namespace MooBoo.Infrastructure.Interfaces.DataLayer
{
    public abstract class DataObject : ICloneable<DataObject>
    {
        public abstract DataObject Clone();
        
        public int Id { get; set; }

        
    }
}
