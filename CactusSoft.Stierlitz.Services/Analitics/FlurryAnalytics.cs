using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CactusSoft.Stierlitz.Common;
using CactusSoft.Stierlitz.Services.Web.ProxyServers.Infrastructure;
using FlurryWP7SDK;
using FlurryWP7SDK.Models;


namespace CactusSoft.Stierlitz.Services.Analitics
{
    public sealed class FlurryAnalytics : IAnalyticsService
    {
        private readonly string _apiKey;
        public FlurryAnalytics(IApplicationConfiguration applicationConfiguration)
        {
            _apiKey = applicationConfiguration.FlurryApiKey;
        }

        public void StartSession()
        {
            Api.StartSession(_apiKey);
            Api.SetVersion(Assembly.GetExecutingAssembly().GetVersion());
            LogEvent("Language", "language", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
        }

        public void EndSession()
        {
            Api.EndSession();
        }

        public void ScreenWasDisplayed(ScreenName fromPage, ScreenName toPage)
        {
            if (toPage == ScreenName.None)
            {
                return;
            }
            Api.LogPageView();
            if (fromPage != ScreenName.None)
            {
                LogEvent("Screen Was Displayed", new Dictionary<string, string>
                                                     {
                                                         {"from page", fromPage.ToDescriptionString()},
                                                         {"to page", toPage.ToDescriptionString()}
                                                     });
                return;
            }
            LogEvent("Screen Was Displayed", "to page", toPage.ToDescriptionString());
        }

        public void UnhandledException(Exception exception)
        {
            Api.LogError("Unhandled exception", exception);
        }

        public void Login(bool rememberMe)
        {
            LogEvent("Login", "remember me", rememberMe.ToString().ToLower());
        }

        public void Logout()
        {
            LogEvent("Logout");
        }

        public void Update(ScreenName screenName)
        {
            LogEvent("Update", "screen", screenName.ToDescriptionString());
        }

        public void AddTriggersToFavorites()
        {
            LogEvent("Add trigger to favorites");
        }

        public void AddGraphToFavorites()
        {
            LogEvent("Add graph to favorites");
        }

        public void ContactUs()
        {
            LogEvent("Contact us");
        }

        public void AddServer()
        {
            LogEvent("Add server");
        }

        public void NextPeriod()
        {
            LogEvent("Graph change next period");
        }

        public void PreviousPeriod()
        {
            LogEvent("Graph change previous period");
        }

        public void ChangePeriod(TimeSpan period)
        {
            LogEvent("Graph change period", "period(h)", period.TotalHours.ToString(CultureInfo.InvariantCulture));
        }

        private void LogEvent(string eventName, IDictionary<string, string> parameters)
        {
            Api.LogEvent(eventName, parameters.Select(t => new Parameter(t.Key, t.Value)).ToList());
            Debug.WriteLine("Flurry Log: {0}: {1}", eventName, parameters.Aggregate(string.Empty, (acc, pair) => string.Format("{0}; {1} - {2}", acc, pair.Key, pair.Value)));
        }

        private void LogEvent(string eventName, string parameter, string value)
        {
            Api.LogEvent(eventName, new List<Parameter> { new Parameter(parameter, value) });
            Debug.WriteLine("Flurry Log: {0}: {1} - {2}", eventName, parameter, value);
        }

        private void LogEvent(string eventName)
        {
            Api.LogEvent(eventName);
            Debug.WriteLine("Flurry Log: {0}", eventName);
        }
    }
}
