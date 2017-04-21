using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class Trigger : IEquatable<Trigger>
    {
        [DataMember]
        public string Id
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool IsOk
        {
            get;
            set;
        }

        public TriggerPriority Priority
        {
            get;
            set;
        }

        public IEnumerable<Host> Hosts
        {
            get;
            set;
        }


        public bool Equals(Trigger other)
        {
            return Id == other.Id;
        }
    }
}
