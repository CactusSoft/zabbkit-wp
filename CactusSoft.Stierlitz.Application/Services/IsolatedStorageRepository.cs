using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class IsolatedStorageRepository<T> : IIsolatedStorageRepository<T>
    {
        private readonly string _namePrefix;

        public IsolatedStorageRepository(string namePrefix)
        {
            _namePrefix = namePrefix;
        }

        public IsolatedStorageRepository() : this(string.Empty)
        {
            
        }

        private string FileName
        {
            get { return string.Format("{0}.{1}.data.json", _namePrefix, typeof(T)); }
        }

        public void Store(T obj)
        {
            using (IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication())
            {

                using (IsolatedStorageFileStream fileStream = appStore.OpenFile(FileName, FileMode.Create))
                {
                    var serializer = new DataContractSerializer(typeof (T));
                    serializer.WriteObject(fileStream, obj);
                }
            }
        }
        public T Retrieve()
        {
            T obj = default(T);
            using (IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (appStore.FileExists(FileName))
                {
                    using (IsolatedStorageFileStream fileStream = appStore.OpenFile(FileName, FileMode.Open))
                    {
                        var serializer = new DataContractSerializer(typeof (T));
                        obj = (T) serializer.ReadObject(fileStream);
                    }
                }
                return obj;
            }
        }
    }
}
