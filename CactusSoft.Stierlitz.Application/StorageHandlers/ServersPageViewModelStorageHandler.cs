using CactusSoft.Stierlitz.Application.StorageHandlers.Base;
using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class ServersPageViewModelStorageHandler : ScreenViewModelStorageHandler<ServersPageViewModel>
	{
		public override void Configure()
		{
            Property(vm => vm.Name).InPhoneState();
            Property(vm => vm.Uri).InPhoneState();
		}
	}
}