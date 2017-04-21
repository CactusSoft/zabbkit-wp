using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Common
{
    public interface INetworkStateManager
    {
        Task<bool> GetIsNetworkAvailableAsync();

        bool IsNetworkConnected { get; }
    }
}
