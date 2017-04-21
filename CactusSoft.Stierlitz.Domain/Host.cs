using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class Host
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Id
        {
            get;
            set;
        }
    }
}
