using CactusSoft.Stierlitz.Services.JsonConverters;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base
{
    [JsonObject]
    public abstract class GetParams
    {
        [JsonProperty(PropertyName = "output")]
        [JsonConverter(typeof(OutputQueryConverter))]
        public Query Output
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "limit")]
        public uint? Limit
        {
            get;
            set;
        }
    }
}
