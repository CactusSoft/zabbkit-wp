using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public class NavigationServiceResolver : INavigationServiceResolver
    {
        private const string NAVIGATION_URI_KEY = "navigation_uri_key";
        private readonly PhoneApplicationFrame _navigationService;

        public NavigationServiceResolver(PhoneApplicationFrame navigationService)
        {
            _navigationService = navigationService;
        }

        private Uri NavigationUri
        {
            get;
            set;
        }

        public bool TryResolve()
        {
            if (NavigationUri == null)
            {
                return false;
            }

            try
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            _navigationService.Navigate(NavigationUri);
                            NavigationUri = null;
                        });
                
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Preserve()
        {
            PhoneApplicationService.Current.State[NAVIGATION_URI_KEY] = NavigationUri;
        }

        public void TryRestore()
        {
            _navigationService.NavigationFailed -= OnNavigationFailed;
            _navigationService.NavigationFailed += OnNavigationFailed;

            if (PhoneApplicationService.Current.State.ContainsKey(NAVIGATION_URI_KEY))
            {
                NavigationUri = (Uri) PhoneApplicationService.Current.State[NAVIGATION_URI_KEY];
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (!(e.Exception is InvalidOperationException &&
                e.Exception.Message.Contains("Navigation is not allowed when the task is not in the foreground")))
            {
                return;
            }

            e.Handled = true;
            NavigationUri = e.Uri;
        }
    }
}
