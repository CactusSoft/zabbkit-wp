using CactusSoft.Stierlitz.Application.StorageHandlers.Base;
using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class EventsPageViewModelStorageHandler : ScreenViewModelStorageHandler<EventsPageViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.TriggerId).InPhoneState();
        }
    }
}