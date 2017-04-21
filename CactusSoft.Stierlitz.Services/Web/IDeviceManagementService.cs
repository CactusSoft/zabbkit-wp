using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Services.Web
{
    /// <summary>
    /// notifications management
    /// </summary>
    public interface IDeviceManagementService
    {
        /// <summary>
        /// register device at our device service
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> RegisterDevice(string url);
    }
}
