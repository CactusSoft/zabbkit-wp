using System.Threading.Tasks;
using CactusSoft.Stierlitz.Common;
using NetworkInterface = System.Net.NetworkInformation.NetworkInterface;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public class NetworkStateManager : INetworkStateManager
    {
        public async Task<bool> GetIsNetworkAvailableAsync()
        {
            return await Task<bool>.Factory.StartNew(() => IsNetworkConnected);
        }

        public bool IsNetworkConnected
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }
    }
}
