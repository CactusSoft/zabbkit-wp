using System;
using System.Collections.Generic;
using CactusSoft.Stierlitz.Services.Web.RequestBodies.Params;

namespace CactusSoft.Stierlitz.Services.Web.Configurations
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        private bool _isInitialize;
        private readonly IDictionary<Type, string> _methods = new Dictionary<Type, string>();

        public string ApiPath
        {
            get { return "/api_jsonrpc.php"; }
        }

        public string GraphPathFormat
        {
            get
            {
                return "/chart2.php?graphid={0}&height={1}&width={2}&stime={3}&period={4}";
            }
        }

        public string Version
        {
            get { return "2.0"; }
        }

        private void RegisterMethods()
        {
            RegisterMethod<LoginParams>("user.login");
            RegisterMethod<LogoutParams>("user.logout");
            RegisterMethod<GetHostGroupsParams>("hostgroup.get");
            RegisterMethod<GetHostsParams>("host.get");
            RegisterMethod<GetTriggersParams>("trigger.get");
            RegisterMethod<GetEventsParams>("event.get");
            RegisterMethod<GetGraphsParams>("graph.get");
            RegisterMethod<GetDataParams>("item.get");
            RegisterMethod<ApiVersionParams>("apiinfo.version");

            _isInitialize = true;
        }

        public string ResolveMethod<T>()
        {
            if (!_isInitialize)
            {
                RegisterMethods();
            }

            if (!_methods.ContainsKey(typeof(T)))
            {
                throw new ArgumentException(string.Format("Method for {0} isn't registered.", typeof(T)));
            }

            return _methods[typeof(T)];
        }

        private void RegisterMethod<T>(string method)
        {
            _methods[typeof(T)] = method;
        }
    }
}

