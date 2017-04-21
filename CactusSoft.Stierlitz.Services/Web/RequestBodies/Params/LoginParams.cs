using Newtonsoft.Json;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    [JsonObject]
    public class LoginParams
    {
        [JsonProperty(PropertyName = "user")]
        public string Login
        {
            get;
            internal set;
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get;
            internal set;
        }

    }
}
