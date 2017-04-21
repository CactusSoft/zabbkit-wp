using CactusSoft.Stierlitz.Services.Web.Configurations;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies
{
    public class RequestBodyBuilder : IRequestBodyBuilder
    {
        private readonly IWebConfiguration _webConfiguration;
        private readonly IServiceConfiguration _serviceConfiguration;

        public RequestBodyBuilder(IWebConfiguration webConfiguration, IServiceConfiguration serviceConfiguration)
        {
            _webConfiguration = webConfiguration;
            _serviceConfiguration = serviceConfiguration;
        }

        public ParamsRequestBody<T> Build<T>(T requestParams)
        {
            return new ParamsRequestBody<T>()
                       {
                           JsonRpc = _serviceConfiguration.Version,
                           Method = _serviceConfiguration.ResolveMethod<T>(),
                           AccessToken = _webConfiguration.AccessToken,
                           Params = requestParams,
                           Id = 1
                       };
        }
    }
}
