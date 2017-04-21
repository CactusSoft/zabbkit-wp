using System;

namespace CactusSoft.Stierlitz.Application.Messages
{
    public class PeriodChangedMessage
    {
        public DateTime? StartTime
        {
            get;
            set;
        }
        public TimeSpan Period
        {
            get;
            set;
        }
    }
}
