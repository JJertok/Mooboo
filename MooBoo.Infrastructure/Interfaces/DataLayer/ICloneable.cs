namespace MooBoo.Infrastructure.Interfaces.DataLayer
{
    public interface ICloneable<out T> where T : class
    {
        T Clone();
    }
}
