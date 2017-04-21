using System.Linq;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IEventProxyServer
    {
        Task<IOrderedEnumerable<Event>> GetEvents(string triggerId, uint? limit, EventSortField sortBy, Select selectHosts, Select selectTriggers);
    }
}
