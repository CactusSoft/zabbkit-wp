using System.Collections.Generic;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly ISettingsStorage _settingsStorage;

        public ApplicationSettings(ISettingsStorage settingsStorage)
        {
            _settingsStorage = settingsStorage;

            if (Servers == null)
            {
                Servers = new Dictionary<string, Server>();
            }

            if (UserNames == null)
            {
                UserNames = new Dictionary<string, string>();
            }
        }

        public IDictionary<string, Server> Servers
        {
            get
            {
                return _settingsStorage.GetValue(() => Servers, null);
            }

            private set
            {
                _settingsStorage.SetValue(() => Servers, value);
            }
        }

        public IDictionary<string, string> UserNames
        {
            get
            {
                return _settingsStorage.GetValue(() => UserNames, null);
            }

            private set
            {
                _settingsStorage.SetValue(() => UserNames, value);
            }
        }

        public bool RememberMe
        {
            get
            {
                return _settingsStorage.GetValue(() => RememberMe, false);
            }

            set
            {
                _settingsStorage.SetValue(() => RememberMe, value);
            }
        }

        public UserCredentials UserCredentials
        {
            get
            {
                var userCredentials = _settingsStorage.GetValue(() => UserCredentials, null);
                return UserCredentialsEncryptService.Unprotect(userCredentials);
            }

            set
            {
                var userCredentials = UserCredentialsEncryptService.Protect(value);
                _settingsStorage.SetValue(() => UserCredentials, userCredentials);
            }
        }

        public string LastSelectedServerName
        {
            get
            {
                return _settingsStorage.GetValue(() => LastSelectedServerName, null);
            }
            set
            {
                _settingsStorage.SetValue(() => LastSelectedServerName, value);
            }
        }
    }
}