using System;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies;
using CactusSoft.Stierlitz.Services.Web.WebChannel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public class ZabbixUserProxyServer : ZabbixProxyServerBase, IUserProxyServer
    {
        public ZabbixUserProxyServer(IWebConfiguration webConfiguration, IWebChannel webChannel, IRequestBodyBuilder requestBodyBuilder) :
            base(webConfiguration, webChannel, requestBodyBuilder)
        {
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var loginParams = new LoginParams { Login = userName, Password = password };
            ParamsRequestBody<LoginParams> loginRequestBody = RequestBodyBuilder.Build(loginParams);
            ResultResponseBody<string> loginResponseBody = await WebChannel.GetResponseAsync<ParamsRequestBody<LoginParams>, ResultResponseBody<string>>(loginRequestBody);
                
            if (loginResponseBody.Error  != null)
            {
                throw new AuthorizationException(loginResponseBody.Error.Code, loginResponseBody.Error.Message);
            }

            return loginResponseBody.Result;
        }

        public async Task<bool> LogoutAsync()
        {
            ParamsRequestBody<LogoutParams> logoutRequestBody = RequestBodyBuilder.Build(new LogoutParams());

            ResultResponseBody<bool> logoutResponseBody =
                await WebChannel.GetResponseAsync<ParamsRequestBody<LogoutParams>, ResultResponseBody<bool>>(logoutRequestBody);

            if (logoutResponseBody.Error != null)
            {
                throw new WebServiceException(logoutResponseBody.Error.Code, logoutResponseBody.Error.Message);
            }

            return logoutResponseBody.Result;
        }
    }
}
