using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Messages
{
    public abstract class ServersChangedMessage
    {
        public Server Server
        {
            get;
            set;
        }
    }
}