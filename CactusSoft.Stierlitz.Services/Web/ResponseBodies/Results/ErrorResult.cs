using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results
{
    [JsonObject]
    public class ErrorResult
    {
        [JsonProperty(PropertyName = "code")]
        public int Code
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "data")]
        public string Data
        {
            get;
            set;
        }
    }
}
