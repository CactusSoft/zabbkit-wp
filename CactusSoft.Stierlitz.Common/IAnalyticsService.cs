using System;

namespace CactusSoft.Stierlitz.Common
{
    public interface IAnalyticsService
    {
        void StartSession();
        void EndSession();

        void ScreenWasDisplayed(ScreenName fromPage, ScreenName toPage);
        void UnhandledException(Exception exception);

        void Login(bool rememberMe);
        void Logout();
        void Update(ScreenName screenName);
        void AddTriggersToFavorites();
        void AddGraphToFavorites();
        void ContactUs();
        void AddServer();
        void NextPeriod();
        void PreviousPeriod();
        void ChangePeriod(TimeSpan period);
    }
}
