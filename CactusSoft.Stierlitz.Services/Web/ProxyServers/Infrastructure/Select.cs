using System.ComponentModel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure
{
    public enum Select
    {
        None,
        [Description("shorten")]
        Shorten,
        [Description("refer")]
        Refer,
        [Description("extend")]
        Extend,
        [Description("count")]
        Count
    }
}
