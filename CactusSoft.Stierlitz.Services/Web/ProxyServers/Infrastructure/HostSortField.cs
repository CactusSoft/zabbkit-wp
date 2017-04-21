using System.ComponentModel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure
{
    public enum HostSortField
    {
        [Description("name")] 
        ByName,

        [Description("hostid")] 
        ByHostId,

        [Description("status")] 
        ByStatus,
    }
}
