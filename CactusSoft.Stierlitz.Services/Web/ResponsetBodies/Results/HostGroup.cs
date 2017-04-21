using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Services.Web.ResponsetBodies.Results
{
    [DataContract]
    public class HostGroup
    {
        [DataMember(Name = "groupid")]
        public int GroupId
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }
    }
}
