using CactusSoft.Stierlitz.Application.StorageHandlers.Base;
using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class LoginPageViewModelStorageHandler : ScreenViewModelStorageHandler<LoginPageViewModel>
	{
		public override void Configure()
		{
            Property(vm => vm.Username).InPhoneState();
            Property(vm => vm.Password).InPhoneState();
            Property(vm => vm.RememberMe).InPhoneState();
            Property(vm => vm.SelectedServerName).InPhoneState();
		}
	}
}