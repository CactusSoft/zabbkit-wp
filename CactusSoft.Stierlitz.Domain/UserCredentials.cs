using System.Runtime.Serialization;

namespace CactusSoft.Stierlitz.Domain
{
    [DataContract]
    public class UserCredentials
    {
        private string _password;

        [DataMember]
        public string Username
        {
            get;
            set;
        }

        [DataMember]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        [DataMember]
        public string ServerUri
        {
            get;
            set;
        }
    }
}
