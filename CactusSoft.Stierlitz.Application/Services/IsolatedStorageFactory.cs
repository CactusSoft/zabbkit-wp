using System;
using System.Security.Cryptography;
using System.Text;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.Configurations;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class IsolatedStorageFactory : IIsolatedStorageFactory
    {
        private readonly IWebConfiguration _webConfiguration;

        public IsolatedStorageFactory(IWebConfiguration webConfiguration)
        {
            _webConfiguration = webConfiguration;
        }

        public IIsolatedStorageRepository<T> CreateRepository<T>()
        {
            return new IsolatedStorageRepository<T>(GetPrefix());
        }

        private string GetPrefix()
        {
            var prefix = string.Concat(_webConfiguration.ServerUri, ".", _webConfiguration.UserName);
            using (var sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(prefix));
                return Convert.ToBase64String(hash).Replace("+", "_").Replace("/", "-").Replace("=", "");
            }
        }
    }
}
