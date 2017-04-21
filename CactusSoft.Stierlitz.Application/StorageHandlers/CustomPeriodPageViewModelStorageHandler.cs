using CactusSoft.Stierlitz.Application.ViewModels.Period;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class CustomPeriodPageViewModelStorageHandler : StorageHandler<CustomPeriodPageViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.Date).InPhoneState();
            Property(vm => vm.Time).InPhoneState();
            Property(vm => vm.Period).InPhoneState();
        }
    }
}
