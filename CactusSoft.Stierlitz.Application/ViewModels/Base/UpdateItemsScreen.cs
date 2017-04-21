using System.Diagnostics;
using CactusSoft.Stierlitz.Common;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class UpdateItemsScreen<T> : LoadItemsScreen<T>, IUpdate
    {
        protected readonly INavigationService NavigationService;

        protected UpdateItemsScreen(IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, INavigationService navigationService)
            : base(busyIndicatorManager, errorHandler)
        {
            NavigationService = navigationService;
        }

        public override bool IsBusy
        {
            set
            {
                base.IsBusy = value;
                NotifyOfPropertyChange(() => CanUpdate);
            }
        }

        public bool CanUpdate
        {
            get
            {
                return !IsBusy;
            }
        }

        public virtual void Update()
        {
            LoadItemsAsync();
            Debug.WriteLine("Update: {0}", this.GetType());
        }
    }
}
