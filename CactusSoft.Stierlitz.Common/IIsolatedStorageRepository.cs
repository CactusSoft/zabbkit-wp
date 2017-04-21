namespace CactusSoft.Stierlitz.Common
{
    public interface IIsolatedStorageRepository<T>
    {
        void Store(T obj);
        T Retrieve();
    }
}
