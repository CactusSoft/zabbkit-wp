using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetHostsResult
    {
        [JsonProperty(PropertyName = "hostid")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }
    }
}
