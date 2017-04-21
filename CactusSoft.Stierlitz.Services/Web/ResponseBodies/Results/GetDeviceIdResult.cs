using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetDeviceIdResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get;
            set;
        }
    }
}
