using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies
{
    [JsonObject]
    public abstract class RequestBodyBase
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JsonRpc
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "method")]
        public string Method
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "auth")]
        public string AccessToken
        {
            get;
            set;
        }
    }
}
