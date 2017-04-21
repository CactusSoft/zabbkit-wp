using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Localization;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using Caliburn.Micro;

namespace CactusSoft.Stierlitz.Application.ViewModels.MainHub
{
    public class TimelineViewModel : EventsScreen, IMainHubScreen
    {
        private const int DEFAULT_ITEMS_COUNT = 3;
        private readonly INavigationService _navigationService;
        private readonly IAnalyticsService _analyticsService;
        private bool _isBusy;

        public TimelineViewModel(IEventProxyServer eventProxyServer, INavigationService navigationService,
                                 IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(eventProxyServer, null, errorHandler)
        {
            _navigationService = navigationService;
            _analyticsService = analyticsService;
        }

        public override string DisplayName
        {
            get
            {
                return AppResources.TimelineTitle;
            }
        }

        public override bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public void NavigateToDayEvents()
        {
            _navigationService
                .UriFor<TimelinePageViewModel>()
                .Navigate();
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.EventsView);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            IList<Event> events;

            try
            {
                events = (await Executer.Execute(() => EventProxyServer.GetEvents(null, DEFAULT_ITEMS_COUNT, EventSortField.EventId, 
                                                                                  Select.Extend, Select.Extend))).ToList();
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

            Items = InitDuration(events);
        }
	}
}
