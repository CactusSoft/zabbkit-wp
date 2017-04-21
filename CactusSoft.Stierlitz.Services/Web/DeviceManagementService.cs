using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results;
using Newtonsoft.Json;
using INetworkStateManager = CactusSoft.Stierlitz.Common.INetworkStateManager;

namespace CactusSoft.Stierlitz.Services.Web
{
    public class DeviceManagementService : IDeviceManagementService
    {
        private readonly IWebConfiguration _webConfiguration;
        private readonly INetworkStateManager _networkStateManager;

        public DeviceManagementService(IWebConfiguration webConfiguration, 
            INetworkStateManager networkStateManager)
        {
            _webConfiguration = webConfiguration;
            _networkStateManager = networkStateManager;
        }

        public async Task<string> RegisterDevice(string url)
        {
            var device = new DeviceParams
                             {
                                 Type = "WP",
                                 Token = url
                             };
            var result = await GetResponseAsync<DeviceParams, GetDeviceIdResult>("Devices", device);
            return result.Id;
        }

        public async Task<TOut> GetResponseAsync<TIn, TOut>(string path, TIn requestBody) where TIn : class
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException("requestBody");
            }

            var requestUri = new Uri(string.Format("{0}/{1}", 
                _webConfiguration.NotificationServerApiUri, path));

            WebRequest webRequest = WebRequest.CreateHttp(requestUri);

            webRequest.Method = "Post";
            webRequest.ContentType = "application/json";

            using (var requestStream = await webRequest.GetRequestStreamAsync())
            using (var requestStreamWriter = new StreamWriter(requestStream))
            using (var jsonWriter = new JsonTextWriter(requestStreamWriter))
            {
                var jsonSerializer = new JsonSerializer();
                await Task.Factory.StartNew(() => jsonSerializer.Serialize(jsonWriter, requestBody));
            }

            WebResponse webResponse = null;

            WebException exception = null;
            try
            {
                webResponse = await webRequest.GetResponseAsync();
            }
            catch (WebException e)
            {
                exception = e;
            }
            if (exception != null)
            {
                if (!(await IsNetworkConnectedAsync(exception)))
                {
                    throw new NoInternetException("No internet connection.", exception);
                }
                throw exception;
            }

            using (var responseStream = webResponse.GetResponseStream())
            {
                using (var responseStreamReader = new StreamReader(responseStream, true))
                using (var jsonReader = new JsonTextReader(responseStreamReader))
                {

                    var jsonSerializer = new JsonSerializer();
                    try
                    {
                        return await Task<TOut>.Factory.StartNew(() => jsonSerializer.Deserialize<TOut>(jsonReader));
                    }
                    catch (JsonReaderException e)
                    {
                        throw new ResponseParseException(e.Message, e);
                    }
                }
            }
        }

        private async Task<bool> IsNetworkConnectedAsync(WebException e)
        {
            if (e.Response != null && e.Response.ContentLength > 0)
            {
                return true;
            }
            return await _networkStateManager.GetIsNetworkAvailableAsync();
        }

    }
}
