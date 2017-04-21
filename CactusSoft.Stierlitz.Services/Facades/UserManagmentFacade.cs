using System;
using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.Configurations;
using CactusSoft.Stierlitz.Services.Web.Exceptions;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;

namespace CactusSoft.Stierlitz.Services.Facades
{
    public class UserManagmentFacade : IUserManagmentFacade
    {
        private readonly IUserProxyServer _userProxyServer;
        private readonly IWebConfiguration _webConfiguration;
        private readonly IFavoritesStorage<Trigger> _triggerFavoritesStorage;
        private readonly IFavoritesStorage<Graph> _graphFavoritesStorage;

        public UserManagmentFacade(IUserProxyServer userProxyServer, IWebConfiguration webConfiguration, IFavoritesStorage<Trigger> triggerFavoritesStorage, IFavoritesStorage<Graph> graphFavoritesStorage)
        {
            _userProxyServer = userProxyServer;
            _webConfiguration = webConfiguration;
            _triggerFavoritesStorage = triggerFavoritesStorage;
            _graphFavoritesStorage = graphFavoritesStorage;
        }

        public async Task LoginAsync(UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                throw new ArgumentNullException("userCredentials");
            }

            _webConfiguration.ServerUri = userCredentials.ServerUri;
            var token = await _userProxyServer.LoginAsync(userCredentials.Username, userCredentials.Password);
            _webConfiguration.AccessToken = token;
            _webConfiguration.UserName = userCredentials.Username;

            _triggerFavoritesStorage.Init();
            _graphFavoritesStorage.Init();
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _userProxyServer.LogoutAsync();
            }
            catch (WebServiceException)
            {
                // Zabbix can't make logout (Bug: ZBX-3907)
                // Fixed in 2.0.4rc1 r30542, pre-2.1.0 r30543
            }
            catch (Exception)
            {
            }
            finally
            {
                _webConfiguration.AccessToken = null;

                _triggerFavoritesStorage.Clean();
                _graphFavoritesStorage.Clean();
            }

        }
    }
}
