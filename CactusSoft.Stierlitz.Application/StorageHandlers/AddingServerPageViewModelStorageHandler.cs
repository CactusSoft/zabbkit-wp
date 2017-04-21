using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class AddingServerPageViewModelStorageHandler : ScreenViewModelStorageHandler<AddingServerPageViewModel>
	{
		public override void Configure()
		{
            Property(x => x.Name).InPhoneState();
            Property(x => x.Uri).InPhoneState();
		}
	}
}