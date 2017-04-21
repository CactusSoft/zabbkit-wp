using System;
using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class Item : IEquatable<Item>
    {
        [DataMember]
        public string ItemId
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string HostId
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }

        [DataMember]
        public int ValueType
        {
            get;
            set;
        }

        [DataMember]
        public string Units
        {
            get;
            set;
        }


        [DataMember]
        public string GroupId
        {
            get;
            set;
        }


        public bool Equals(Item other)
        {
            return ItemId == other.ItemId;
        }
    }
}
