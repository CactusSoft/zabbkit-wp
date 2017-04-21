using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class DeviceParams
    {
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "token")]
        public string Token
        {
            get;
            internal set;
        }

    }
}
