using System.Collections.Generic;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface ITriggerProxyServer
    {
        Task<IEnumerable<Trigger>> GetTriggers(string hostId, uint? limit, TriggersSortField[] sortFields,
                                               Select selectHosts, IList<string> triggerIds = null);
    }
}
