using System.Collections.Generic;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IHostGroupProxyServer
    {
        Task<IEnumerable<HostGroup>> GetHostGroups(HostGroupsSortField[] sortFields, uint? limit);
    }
}
