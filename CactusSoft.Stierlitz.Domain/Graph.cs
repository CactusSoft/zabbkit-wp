using System;
using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class Graph : IEquatable<Graph>
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string GraphId
        {
            get;
            set;
        }

        public bool Equals(Graph other)
        {
            return GraphId == other.GraphId;
        }
    }
}
