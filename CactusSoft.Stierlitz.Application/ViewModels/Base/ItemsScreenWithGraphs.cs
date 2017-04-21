using CactusSoft.Stierlitz.Common;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class ItemsScreenWithGraphs<T> : UpdateItemsScreen<T>
    {
        protected ItemsScreenWithGraphs(IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, INavigationService navigationService) : base(busyIndicatorManager, errorHandler, navigationService)
        {
        }

        public override bool IsBusy
        {
            set
            {
                base.IsBusy = value;
                NotifyOfPropertyChange(() => CanNavigateToGraphs);
                NotifyOfPropertyChange(() => CanNavigateToData);
            }
        }

        public bool CanNavigateToGraphs
        {
            get
            {
                return !IsBusy;
            }
        }

        public bool CanNavigateToData
        {
            get
            {
                return !IsBusy;
            }
        }


        public abstract void NavigateToGraphs();

        public abstract void NavigateToData();
    }
}
