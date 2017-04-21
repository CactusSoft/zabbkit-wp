using System;
using System.Collections.Generic;
using System.Linq;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ProxyServers;

namespace CactusSoft.Stierlitz.Application.ViewModels.Base
{
    public abstract class EventsScreen : UpdateItemsScreen<EventViewModel>
    {
        protected readonly IEventProxyServer EventProxyServer;

        protected EventsScreen(IEventProxyServer eventProxyServer, IGlobalBusyIndicatorManager busyIndicatorManager, 
            IErrorHandler errorHandler) : base(busyIndicatorManager, errorHandler, null)
        {
            EventProxyServer = eventProxyServer;
        }

        protected IEnumerable<EventViewModel> InitDuration(IList<Event> events, TriggerPriority priority)
        {
            var result = new List<EventViewModel>();
            if (events.Any())
            {
                Event @event = events.First();
                var eventViewModel = new EventViewModel(@event, GetPriority(@event, priority), DateTime.Now);
                result.Add(eventViewModel);
            }
            for (int i = 1; i < events.Count(); ++i)
            {
                Event @event = events[i];
                var eventViewModel = new EventViewModel(@event, GetPriority(@event, priority), events[i - 1].Time);
                result.Add(eventViewModel);
            }
            return result;
        }

        protected IEnumerable<EventViewModel> InitDuration(IList<Event> events)
        {
            var eventsViewModels = new List<EventViewModel>();
            
            var groupedEvents = events.GroupBy(e => e.Trigger != null ? e.Trigger.Id : "");
            foreach (var @event in groupedEvents)
            {
                if (@event.Key == "")
                {
                    continue;
                }
                eventsViewModels.AddRange(InitDuration(@event.ToList(), TriggerPriority.NotClassified));
            }

            return eventsViewModels.OrderByDescending(e => e.BeginTime);
        }

        private TriggerPriority GetPriority(Event @event, TriggerPriority defaultPriority)
        {
            if (@event.Trigger == null)
            {
                return defaultPriority;
            }

            return @event.Trigger.Priority;
        }
    }
}
