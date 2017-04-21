using System.Collections.Generic;
using CactusSoft.Stierlitz.Services.JsonConverters;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetTriggersParams : GetSortedParams
    {
        [JsonProperty(PropertyName = "hostids")]
        public string HostId
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "limit")]
        public new uint? Limit
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "monitored")]
        public bool? ActiveOnly
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "selectHosts")]
        [JsonConverter(typeof(OutputQueryConverter))]
        public Query SelectHosts
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "expandDescription")]
        public bool? ExpandDescription
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "triggerids")]
        public IList<string> TriggerIds
        {
            get;
            set;
        }
    }
}
