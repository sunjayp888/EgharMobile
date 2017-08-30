namespace Egharpay.Data.Interfaces
{
    public interface IDatabaseFactory<T>
    {
        T CreateContext();
    }
}
