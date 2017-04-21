using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies
{
    [JsonObject]
    public class ResultResponseBody<T> : ResponseBodyBase
    {
        [JsonProperty(PropertyName = "result")]
        public T Result
        {
            get;
            set;
        }
    }
}
