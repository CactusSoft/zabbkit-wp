using System.Collections.ObjectModel;

namespace CactusSoft.Stierlitz.Services.Facades
{
    public interface IFavoritesStorage<T>
    {
        void Add(T item);
        bool Exists(T item);
        bool Any();
        ReadOnlyCollection<T> Favorites();
        void Remove(T item);
        void Init();
        void Clean();
    }
}
