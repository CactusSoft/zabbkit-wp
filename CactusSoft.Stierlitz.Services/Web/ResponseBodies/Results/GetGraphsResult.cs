using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetGraphsResult
    {
        [JsonProperty(PropertyName = "graphid")]
        public string GraphId
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
