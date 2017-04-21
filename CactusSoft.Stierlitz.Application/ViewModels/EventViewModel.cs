using System;
using System.Collections.Generic;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(Event eventModel, TriggerPriority triggerPriority, DateTime endTime)
        {
            BeginTime = eventModel.Time;
            Duration = endTime - BeginTime;
            IsOk = eventModel.IsOk.HasValue && eventModel.IsOk.Value;
            TriggerPriority = triggerPriority;
            Hosts = eventModel.Hosts;
            Trigger = eventModel.Trigger;
        }

        public Trigger Trigger
        {
            get;
            private set;
        }

        public IEnumerable<Host> Hosts
        {
            get;
            private set;
        }

        public DateTime BeginTime
        {
            get;
            private set;
        }

        public TimeSpan Duration
        {
            get;
            private set;
        }

        public TriggerPriority TriggerPriority
        {
            get;
            private set;
        }

        public bool IsOk
        {
            get;
            private set;
        }
    }
}
