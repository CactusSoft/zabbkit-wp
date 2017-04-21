namespace CactusSoft.Stierlitz.Services.Web.Configurations
{
    public interface IWebConfiguration
    {
        string AccessToken
        {
            get;
            set;
        }

        string ServerApiUri
        {
            get;
        }

        string ServerUri
        {
            get;
            set;
        }

        string UserName
        {
            get;
            set;
        }

        string NotificationServerApiUri
        {
            get;
        }

    }
}
