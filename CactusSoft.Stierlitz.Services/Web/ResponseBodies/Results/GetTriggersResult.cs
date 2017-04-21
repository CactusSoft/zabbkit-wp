using System.Collections.Generic;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetTriggersResult
    {
        [JsonProperty(PropertyName = "triggerid")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "description")]
        public string Description 
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "priority")]
        public uint Priority
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hosts")]
        public IEnumerable<GetHostsResult> Hosts
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "value")]
        public uint State
        {
            get;
            set;
        }
    }
}
