using System.Collections.Generic;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetHostsParams : GetSortedParams
    {
        [JsonProperty(PropertyName = "groupids")]
        public string GroupId
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "with_monitored_httptests")]
        public bool? WithHttpTests
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hostids")]
        public IEnumerable<string> HostIds
        {
            get;
            internal set;
        }

    }
}
