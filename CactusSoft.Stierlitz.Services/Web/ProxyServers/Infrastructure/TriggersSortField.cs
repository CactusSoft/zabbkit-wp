using System.ComponentModel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure
{
    public enum TriggersSortField
    {
        [Description("triggerid")]
        ByTriggerId,

        [Description("description")]
        ByDescription,

        [Description("status")]
        ByStatus,

        [Description("priority")]
        ByPriority,

        [Description("lastchange")]
        ByLastchange,

        [Description("hostname")]
        ByHostname,
    }
}