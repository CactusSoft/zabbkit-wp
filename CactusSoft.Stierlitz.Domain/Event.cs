using System;
using System.Collections.Generic;

namespace CactusSoft.Stierlitz.Domain
{
    public class Event
    {
        public string Id
        {
            get;
            set;
        }

        public bool? IsOk
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public IEnumerable<Host> Hosts
        {
            get;
            set;
        }

        public Trigger Trigger
        {
            get;
            set;
        }
    }
}
