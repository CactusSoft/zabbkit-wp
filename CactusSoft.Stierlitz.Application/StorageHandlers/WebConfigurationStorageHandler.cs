using CactusSoft.Stierlitz.Services.Web.Configurations;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class WebConfigurationStorageHandler : StorageHandler<IWebConfiguration>
    {
        public override void Configure()
        {
            Property(wc => wc.AccessToken).InPhoneState();
            Property(wc => wc.ServerUri).InPhoneState();
            Property(wc => wc.UserName).InPhoneState();
        }
    }
}
