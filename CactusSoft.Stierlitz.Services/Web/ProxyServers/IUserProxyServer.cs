using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IUserProxyServer
    {
        Task<string> LoginAsync(string userName, string password);
        Task<bool> LogoutAsync();
    }
}
