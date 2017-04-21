using System;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.RequestBodies;
using CactusSoft.Stierlitz.Services.Web.WebChannel;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public abstract class ZabbixProxyServerBase
    {
        protected IWebConfiguration WebConfiguration;
        protected IWebChannel WebChannel;
        protected IRequestBodyBuilder RequestBodyBuilder;

        protected ZabbixProxyServerBase(IWebConfiguration webConfiguration, IWebChannel webChannel, IRequestBodyBuilder requestBodyBuilder)
        {
            if (webConfiguration == null)
                throw new ArgumentNullException("webConfiguration");

            if (webChannel == null)
                throw new ArgumentNullException("webChannel");

            if (requestBodyBuilder == null)
                throw new ArgumentNullException("requestBodyBuilder");

            WebConfiguration = webConfiguration;
            WebChannel = webChannel;
            RequestBodyBuilder = requestBodyBuilder;
        }
    }
}
