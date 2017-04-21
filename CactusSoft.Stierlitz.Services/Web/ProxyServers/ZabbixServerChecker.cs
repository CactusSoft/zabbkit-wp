using System;
using System.Net;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;
using CactusSoft.Stierlitz.Services.Web.WebChannel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public class ZabbixServerChecker : IServerChecker
    {
        private readonly IWebChannel _webChannel;
        private readonly IRequestBodyBuilder _requestBodyBuilder;
        private readonly IWebConfiguration _webConfiguration;

        public ZabbixServerChecker(IWebChannel webChannel, IRequestBodyBuilder requestBodyBuilder, IWebConfiguration webConfiguration)
        {
            _webChannel = webChannel;
            _requestBodyBuilder = requestBodyBuilder;
            _webConfiguration = webConfiguration;
        }

        public async Task<bool> CheckUriAsync(string uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            string oldUrl = _webConfiguration.ServerUri;
            _webConfiguration.ServerUri = uri;
            try
            {
                var checkParams = new ApiVersionParams();
                ParamsRequestBody<ApiVersionParams> requestBody = _requestBodyBuilder.Build(checkParams);
                ResultResponseBody<object> responseBody = await
                    _webChannel.GetResponseAsync<ParamsRequestBody<ApiVersionParams>, ResultResponseBody<object>>(requestBody);

                return responseBody.Error == null;
            }
            catch (ResponseParseException)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
            finally
            {
                if (oldUrl != null)
                {
                    _webConfiguration.ServerUri = oldUrl;
                }
            }
        }
    }
}
