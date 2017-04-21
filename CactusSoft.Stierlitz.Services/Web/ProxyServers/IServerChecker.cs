using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Services.Web.ProxyServers
{
    public interface IServerChecker
    {
        Task<bool> CheckUriAsync(string uri);
    }
}
