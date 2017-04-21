using System.Collections.Generic;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetEventsResult
    {
        [JsonProperty(PropertyName = "eventid")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "clock")]
        public uint Timestamp
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

        [JsonProperty(PropertyName = "triggers")]
        public IEnumerable<GetTriggersResult> Triggers
        {
            get;
            set;
        }

        /*
         * 
         * Possible values: 
         * 0 - trigger; 
         * 1 - discovered host; 
         * 2 - discovered service; 
         * 3 - auto-registered host.
         * 
         */
        [JsonProperty(PropertyName = "object")]
        public uint Object
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
