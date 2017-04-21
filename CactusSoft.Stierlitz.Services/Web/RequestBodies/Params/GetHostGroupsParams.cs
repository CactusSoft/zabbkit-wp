using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetHostGroupsParams : GetSortedParams
    {
        [JsonProperty(PropertyName = "with_monitored_httptests")]
        public bool? WithHttpTests
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "monitored_hosts")]
        public bool? MonitoredHosts
        {
            get;
            set;
        }
    }
}
