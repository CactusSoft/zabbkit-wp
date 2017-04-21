using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Navigation;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Application.ViewModels.Period;
using CactusSoft.Stierlitz.Common;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public class AnaliticsNavigationPageLogger
    {
        private readonly INavigationService _navigationService;
        private readonly IAnalyticsService _analyticsService;
        private readonly Dictionary<Uri, ScreenName> _pageNames;  
        private Uri _lastLoggedPage;
        public AnaliticsNavigationPageLogger(INavigationService navigationService, IAnalyticsService analyticsService)
        {
            _navigationService = navigationService;
            _analyticsService = analyticsService;
            navigationService.Navigating += NavigationServiceOnNavigating;
            

            _pageNames = new Dictionary<Uri, ScreenName>
                            {
                                {Uri<LoginPageViewModel>(), ScreenName.LoginPage},
                                {Uri<MainPageViewModel>(), ScreenName.MainPanorama},
                                {Uri<FavoritesPageViewModel>(), ScreenName.FavoritesPage},
                                {Uri<PeriodPageViewModel>(), ScreenName.PeriodPage},
                                {Uri<CustomPeriodPageViewModel>(), ScreenName.CustomPeriodPage},
                                {Uri<AboutPageViewModel>(), ScreenName.AboutPage},
                                {Uri<EventsPageViewModel>(), ScreenName.EventsPage},
                                {Uri<GraphPageViewModel>(), ScreenName.GraphPage},
                                {Uri<GraphsPageViewModel>(), ScreenName.GraphsPage},
                                {Uri<HostsPageViewModel>(), ScreenName.HostsPage},
                                {Uri<HostTriggersPageViewModel>(), ScreenName.HostTriggersPage},
                                {Uri<ServerPickerPageViewModel>(), ScreenName.ServerPickerPage},
                                {Uri<ServersPageViewModel>(), ScreenName.ServerPage},
                                {Uri<HostGroupsPageViewModel>(), ScreenName.AllHostGroupsPage},
                                {Uri<TimelinePageViewModel>(), ScreenName.AllEventsPage},
                                {Uri<TriggersPageViewModel>(), ScreenName.AllTriggersPage}
                            };
        }

        private void NavigationServiceOnNavigating(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            if (navigatingCancelEventArgs.NavigationMode != NavigationMode.New)
            {
                return;
            }
            if (_lastLoggedPage != navigatingCancelEventArgs.Uri || _navigationService.Source == null)
            {
                _lastLoggedPage = navigatingCancelEventArgs.Uri;
                ScreenName fromPage = GetScreenName(_navigationService.Source);
                ScreenName toPage = GetScreenName(navigatingCancelEventArgs.Uri);
                _analyticsService.ScreenWasDisplayed(fromPage, toPage);
            }
            else
            {
                _lastLoggedPage = null;
            }
            
        }



        private ScreenName GetScreenName(Uri uri)
        {
            if (uri == null)
            {
                return ScreenName.None;
            }
            ScreenName screenName;
            string clearUri = uri.ToString();
            int index = clearUri.IndexOf("?", StringComparison.Ordinal);

            clearUri = index > 0 ? clearUri.Substring(0, index) : uri.ToString();
            if (!_pageNames.TryGetValue(new Uri(clearUri, uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative), out screenName))
            {
                return ScreenName.None;
            }
            return screenName;
        }

        private Uri Uri<T>()
        {
            return _navigationService.UriFor<T>().BuildUri();
        }
    }
}
