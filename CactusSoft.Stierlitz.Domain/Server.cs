using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class Server
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Uri
        {
            get;
            set;
        }
    }
}
