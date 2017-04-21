using CactusSoft.Stierlitz.Application.ViewModels;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.StorageHandlers
{
    public class GraphPageViewModelStorageHandler : StorageHandler<GraphPageViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.StartTime).InPhoneState();
            Property(vm => vm.Period).InPhoneState();
        }
    }
}
