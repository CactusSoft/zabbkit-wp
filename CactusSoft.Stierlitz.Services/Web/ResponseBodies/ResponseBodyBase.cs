using CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies
{
    [JsonObject]
    public abstract class ResponseBodyBase
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JsonRpc
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

        [JsonProperty(PropertyName = "error")]
        public ErrorResult Error
        {
            get;
            set;
        }
    }
}
