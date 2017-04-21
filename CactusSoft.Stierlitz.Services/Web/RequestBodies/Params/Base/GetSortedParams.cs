using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base
{
    [JsonObject]
    public abstract class GetSortedParams : GetParams
    {
        [JsonProperty(PropertyName = "sortfield")]
        public string[] SortBy
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "sortorder")]
        public string Order
        {
            get;
            set;
        }
    }
}
