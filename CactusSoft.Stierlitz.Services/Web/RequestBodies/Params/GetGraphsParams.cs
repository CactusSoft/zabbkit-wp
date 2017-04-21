using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetGraphsParams : GetParams
    {
        [JsonProperty(PropertyName = "groupids")]
        public string GroupId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hostids")]
        public string HostId
        {
            get;
            set;
        }
    }
}
