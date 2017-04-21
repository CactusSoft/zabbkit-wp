namespace CactusSoft.Stierlitz.Common
{
    public interface IApplicationConfiguration
    {
        string ApplicationVersion { get; }
        string SupportMail { get; }
        int ReminderRecurrencePerUsageCount
        {
            get;
        }
        string FlurryApiKey
        {
            get;
        }
    }
}
