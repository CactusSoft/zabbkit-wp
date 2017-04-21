using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;
using Newtonsoft.Json;
using INetworkStateManager = CactusSoft.Stierlitz.Common.INetworkStateManager;

namespace CactusSoft.Stierlitz.Services.Web.WebChannel
{
    public class ZabbixWebChannel : IWebChannel
    {
        private readonly IWebConfiguration _webConfiguration;
        private readonly INetworkStateManager _networkStateManager;

        public ZabbixWebChannel(IWebConfiguration webConfiguration, INetworkStateManager networkStateManager)
        {
            _webConfiguration = webConfiguration;
            _networkStateManager = networkStateManager;
        }

        public async Task<TOut> GetResponseAsync<TIn, TOut>(TIn requestBody)
            where TIn : RequestBodyBase
            where TOut : ResponseBodyBase
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException("requestBody");
            }

            if (RequestsUri == null)
            {
                throw new NullReferenceException("RequestsUri is null.");
            }

            WebRequest webRequest = WebRequest.CreateHttp(RequestsUri);
            
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

#if DEBUG
            using (var responseStream = webResponse.GetResponseStream())
            {
                using (var responseStreamReader = new StreamReader(responseStream, true))
                {
                    var s = await responseStreamReader.ReadToEndAsync();
                    Debug.WriteLine("{0} :\n {1}", webRequest.RequestUri, s);

                    using (var textReader = new StringReader(s))
                    {
                        using (var jsonReader = new JsonTextReader(textReader))
                        {
                            var jsonSerializer = new JsonSerializer();
                            try
                            {
                                return await Task<TOut>.Factory.StartNew(() =>
                                                                         jsonSerializer.Deserialize<TOut>(jsonReader));
                            }
                            catch (JsonSerializationException e)
                            {
                                throw new ResponseParseException(e.Message, e);
                            }
                            catch (JsonReaderException e)
                            {
                                throw new ResponseParseException(e.Message, e);
                            }

                        }
                    }
                }
            }

#else
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
#endif

        }

        public async Task<byte[]> Download(string url)
        {
            var webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "Get";

            var cookie = new CookieContainer();
            cookie.Add(new Uri(_webConfiguration.ServerUri), new Cookie("zbx_sessionid", _webConfiguration.AccessToken));
            webRequest.CookieContainer = cookie;

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

            using (var responceStream = webResponse.GetResponseStream())
            {
                return await Task<byte[]>.Factory.StartNew(() =>
                    {
                        var memoryStream = new MemoryStream();
                        responceStream.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    });
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


        private string RequestsUri
        {
            get { return _webConfiguration.ServerApiUri; }
        }


    }
}
