using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.Configurations;

namespace CactusSoft.Stierlitz.Services.Web.WebChannel
{
    public class DeviceManagementWebChannel : WebChannelBase, IDeviceManagementWebChannel
    {
        public DeviceManagementWebChannel(IWebConfiguration webConfiguration, INetworkStateManager networkStateManager) 
            : base(webConfiguration, networkStateManager)
        {
        }

        protected override string RequestsUri
        {
            get { return WebConfiguration.NotificationServerApiUri; }
        }
    }
}
