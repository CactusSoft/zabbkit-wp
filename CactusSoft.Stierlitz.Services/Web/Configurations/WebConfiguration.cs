namespace CactusSoft.Stierlitz.Services.Web.Configurations
{
    public class WebConfiguration : IWebConfiguration
    {
        private readonly IServiceConfiguration _serviceConfiguration;

        public WebConfiguration(IServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public string AccessToken
        {
            get;
            set;
        }

        public string ServerUri
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string NotificationServerApiUri
        {
            get
            {
#if DEBUG
                return "http://vserver.inside.cactussoft.biz/zabbkit-test/api";
#else
                return "http://zabbkit.inside.cactussoft.biz/api";
#endif
            }
        }

        public string ServerApiUri
        {
            get
            {
                return string.Concat(ServerUri, _serviceConfiguration.ApiPath);
            }
        }
    }
}

