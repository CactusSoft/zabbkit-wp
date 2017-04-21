using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies
{
    [JsonObject]
    public class ParamsRequestBody<T> : RequestBodyBase
    {
        [JsonProperty(PropertyName = "params")]
        public T Params
        {
            get;
            set;
        }
    }
}
