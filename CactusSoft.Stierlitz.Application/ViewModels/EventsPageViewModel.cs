using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Facades;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class EventsPageViewModel : EventsScreen
    {
        private readonly IFavoritesStorage<Trigger> _favoritesStorage;
        private readonly IAnalyticsService _analyticsService;
        private Trigger _trigger;

        public EventsPageViewModel(IEventProxyServer eventProxyServer, IGlobalBusyIndicatorManager busyIndicatorManager,
                                   IErrorHandler errorHandler, IFavoritesStorage<Trigger> favoritesStorage, IAnalyticsService analyticsService)
            : base(eventProxyServer, busyIndicatorManager, errorHandler)
        {
            _favoritesStorage = favoritesStorage;
            _analyticsService = analyticsService;
        }

        public string TriggerId
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public TriggerPriority TriggerPriority
        {
            get;
            set;
        }

        public bool IsSubscribed
        {
            get
            {
                return _favoritesStorage.Exists(Trigger);
            }
        }

        public override bool IsBusy
        {
            set
            {
                base.IsBusy = value;
                NotifyOfPropertyChange(() => CanSubscribe);
            }
        }

        public bool CanSubscribe
        {
            get
            {
                return !IsBusy;
            }
        }




        public void Subscribe()
        {
            if (!IsSubscribed)
            {
                _favoritesStorage.Add(Trigger);
                _analyticsService.AddTriggersToFavorites();
            }
            else
            {
                _favoritesStorage.Remove(Trigger);
            }
            NotifyOfPropertyChange(() => IsSubscribed);
        }

        private Trigger Trigger
        {
            get
            {
                return _trigger ?? (_trigger = new Trigger{Id = TriggerId});
            }
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.EventsPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            //uint? triggerId = TriggerId > 0 ? TriggerId : (uint?)null;

            try
            {
                IList<Event> events = (await Executer.Execute(() => EventProxyServer.GetEvents(TriggerId, null, EventSortField.EventId, Select.None, Select.None))).ToList();
                Items = InitDuration(events, TriggerPriority);
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
