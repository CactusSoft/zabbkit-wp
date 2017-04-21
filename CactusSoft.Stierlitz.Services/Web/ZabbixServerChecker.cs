using System.Threading.Tasks;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.Configurations;

namespace CactusSoft.Stierlitz.Services.Web
{
    public class ZabbixServerChecker : IZabbixServerChecker
    {
        private readonly IServiceConfiguration _serviceConfiguration;

        public ZabbixServerChecker(IServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<bool> CheckUriAsync(string uri)
        {
            return true;
        }
    }
}
