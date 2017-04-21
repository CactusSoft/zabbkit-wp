using System.Collections.Generic;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IDataProxyServer
    {
        Task<IList<Item>> GetItemsAsync(string groupId, string hostId);
    }
}
