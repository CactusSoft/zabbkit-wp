using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Common;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class LoadItemsScreenBase<T> : Screen
    {
        protected readonly IErrorHandler ErrorHandler;
        private IEnumerable<T> _items;

        protected LoadItemsScreenBase(IErrorHandler errorHandler)
        {
            ErrorHandler = errorHandler;
        }

        public abstract bool IsBusy
        {
            get;
            set;
        }

        public virtual bool IsEmpty
        {
            get
            {
                return Items != null && !Items.Any();
            }
        }

        public IEnumerable<T> Items
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
                NotifyOfPropertyChange(() => IsEmpty);
            }
        }

        protected abstract void LoadItemsAsync();

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadItemsAsync();
        }
    }
}
