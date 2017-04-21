using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class HostGroup
    {
        [DataMember]
        public string Id
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
    }
}
