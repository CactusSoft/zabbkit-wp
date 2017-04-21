using CactusSoft.Stierlitz.Application.StorageHandlers.Base;
using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class HostsPageViewModelStorageHandler : ScreenViewModelStorageHandler<HostsPageViewModel>
	{
		public override void Configure()
		{
            Property(vm => vm.GroupId).InPhoneState();
		}
	}
}