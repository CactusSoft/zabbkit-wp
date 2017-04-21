namespace CactusSoft.Stierlitz.Common
{
    public interface IMessagingService
    {
        void Alert(string title, string message);
        bool Message(string title, string message);
        void RemindToRateApp();
    }
}
