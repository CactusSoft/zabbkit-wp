using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Services.Facades
{
    public class FavoritesStorage<T> : IFavoritesStorage<T>
    {
        private readonly IIsolatedStorageFactory _isolatedStorageFactory;
        private IIsolatedStorageRepository<List<T>> _isolatedStorageRepository;
        private List<T> _favorites;

        public FavoritesStorage(IIsolatedStorageFactory isolatedStorageFactory)
        {
            _isolatedStorageFactory = isolatedStorageFactory;
        }

        public void Add(T item)
        {
            if (_favorites == null)
                Init();
            if (_favorites != null)
            {
                _favorites.Add(item);
                _isolatedStorageRepository.Store(_favorites);
            }
        }

        public void Remove(T item)
        {
            if (_favorites == null)
                Init();
            if (_favorites != null)
            {
                _favorites.Remove(item);
                _isolatedStorageRepository.Store(_favorites);
            }
        }

        public bool Exists(T item)
        {
            return _favorites != null && _favorites.Contains(item);
        }

        public bool Any()
        {
            return _favorites != null && _favorites.Any();
        }

        public ReadOnlyCollection<T> Favorites()
        {
            if (_favorites == null)
                Init();
            return _favorites != null ? _favorites.AsReadOnly() : null;
        }

        public void Init()
        {
            _isolatedStorageRepository = _isolatedStorageFactory.CreateRepository<List<T>>();
            _favorites = _isolatedStorageRepository.Retrieve() ?? new List<T>();
        }

        public void Clean()
        {
            _isolatedStorageRepository = null;
            _favorites = null;
        }
    }
}
