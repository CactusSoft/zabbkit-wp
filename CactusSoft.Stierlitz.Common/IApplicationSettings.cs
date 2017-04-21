using System.Collections.Generic;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Common
{
    public interface IApplicationSettings
    {
        IDictionary<string, Server> Servers
        {
            get;
        }

        IDictionary<string, string> UserNames
        {
            get;
        }

        bool RememberMe
        {
            get;
            set;
        }

        UserCredentials UserCredentials
        {
            get;
            set;
        }

        string LastSelectedServerName
        {
            get;
            set;
        }
    }
}
