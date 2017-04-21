using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Application.Helpers;
using CactusSoft.Stierlitz.Application.ViewModels.Base;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class TimelinePageViewModel : EventsScreen
    {
        private readonly IAnalyticsService _analyticsService;
        private const uint EVENTS_LIMIT = 250;
        public TimelinePageViewModel(IEventProxyServer eventProxyServer, IGlobalBusyIndicatorManager busyIndicatorManager,
                                      IErrorHandler errorHandler, IAnalyticsService analyticsService)
            : base(eventProxyServer, busyIndicatorManager, errorHandler)
        {
            _analyticsService = analyticsService;
        }

        public override void Update()
        {
            base.Update();
            _analyticsService.Update(ScreenName.AllEventsPage);
        }

        protected override async void LoadItemsAsync()
        {
            IsBusy = true;

            IList<Event> events;

            try
            {
                events = (await Executer.Execute(() => EventProxyServer.GetEvents(null, EVENTS_LIMIT, EventSortField.EventId, 
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
