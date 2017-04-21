using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Facades;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.FavoritesHub
{
    public class TriggersViewModel : TriggersScreen, IFavoritesViewModel
    {
        private readonly IFavoritesStorage<Trigger> _favoritesStorage;


        public TriggersViewModel(ITriggerProxyServer triggerProxyServer, INavigationService navigationService, IGlobalBusyIndicatorManager busyIndicatorManager, IErrorHandler errorHandler, IFavoritesStorage<Trigger> favoritesStorage)
            : base(triggerProxyServer, navigationService, busyIndicatorManager, errorHandler)
        {
            _favoritesStorage = favoritesStorage;
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.TriggersTitle;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return !_favoritesStorage.Any();
            }
        }

        public bool NeedUpdateItems
        {
            get;
            set;
        }

        public override void Update()
        {
            if (NeedUpdateItems)
            {
                LoadItemsAsync();
            }
            NeedUpdateItems = false;
        }

        protected override void OnViewLoaded(object view)
        {
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Update();
        }

        protected override async void LoadItemsAsync()
        {
            if (!_favoritesStorage.Any())
            {
                Items = null;
                return;
            }

            IsBusy = true;

            IEnumerable<Trigger> triggers;

            try
            {
                triggers = await Executer.Execute(() => TriggerProxyServer.GetTriggers(null, null,
                                                                                       null, Select.Extend, _favoritesStorage.Favorites().Select(f => f.Id).ToList()));
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
                return;
            }
            finally
            {
                IsBusy = false;
            }

            Items = triggers.OrderBy(trigger => trigger.IsOk)
                .ThenByDescending(trigger => trigger.Priority)
                .ThenBy(trigger => trigger.Description)
                .ToList();
        }

    }
}
