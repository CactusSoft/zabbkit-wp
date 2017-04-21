using System.Collections.Generic;
using CactusSoft.Stierlitz.Services.JsonConverters;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetEventsParams : GetSortedParams, IFilterParam<Filter>
    {
        [JsonProperty(PropertyName = "triggerids")]
        public string TriggerId
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "time_from")]
        public uint? From
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "filter")]
        public Filter Filter
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "selectHosts")]
        [JsonConverter(typeof(OutputQueryConverter))]
        public Query SelectHosts
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "selectTriggers")]
        [JsonConverter(typeof(OutputQueryConverter))]
        public Query SelectTriggers
        {
            get;
            set;
        }
    }

    [JsonObject]
    public class Filter
    {
        [JsonProperty(PropertyName = "value")]
        public IList<int> Value
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "object")]
        public IList<int> Object
        {
            get;
            set;
        }
    }
} 
