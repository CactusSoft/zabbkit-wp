using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params.Base;
using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class GetDataParams : GetParams
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

        [JsonProperty(PropertyName = "monitored")]
        public bool Monitored
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "expandDescription")]
        public bool ExpandDescription
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "sortField")]
        public string SortField
        {
            get;
            set;
        }


    }
}
