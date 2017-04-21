using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetHostGroupsResult
    {
        [JsonProperty(PropertyName = "groupid")]
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
