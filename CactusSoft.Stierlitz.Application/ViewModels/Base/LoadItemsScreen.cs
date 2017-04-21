using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class LoadItemsScreen<T> : LoadItemsScreenBase<T>
    {
        protected readonly IGlobalBusyIndicatorManager BusyIndicatorManager;

        protected LoadItemsScreen(IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler)
            : base(errorHandler)
        {
            BusyIndicatorManager = busyIndicatorManager;
        }

        public override bool IsBusy
        {
            get
            {
                return BusyIndicatorManager.IsBusy;
            }

            set
            {
                BusyIndicatorManager.IsBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }
    }
}
