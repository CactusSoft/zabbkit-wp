namespace CactusSoft.Stierlitz.Common
{
    public interface IIsolatedStorageFactory
    {
        IIsolatedStorageRepository<T> CreateRepository<T>();
    }
}
