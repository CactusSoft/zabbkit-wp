using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class GetDataResult
    {
        [JsonProperty(PropertyName = "graphid")]
        public string ItemId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "lastvalue")]
        public string Value
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "value_type")]
        public int ValueType
        {
            get;
            set;
        }


        [JsonProperty(PropertyName = "formula")]
        public string Formula
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "key_")]
        public string Key
        {
            get;
            set;
        }


        [JsonProperty(PropertyName = "units")]
        public string Units
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hostid")]
        public string HostId
        {
            get;
            set;
        }

    }
}
