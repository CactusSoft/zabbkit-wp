using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            ApplicationBarInit();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.New && NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (LoginPageViewModel) DataContext;
            ApplicationBar.IsVisible = !viewModel.ValidatingSession;
            viewModel.PropertyChanged += OnPropertyChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (LoginPageViewModel)DataContext;
            viewModel.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "ValidatingSession")
            {
                var viewModel = (LoginPageViewModel)DataContext;
                ApplicationBar.IsVisible = !viewModel.ValidatingSession;
            }  
        }

        private void ApplicationBarInit()
        {
            ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarLogin;
            ((AppBarMenuItem) ApplicationBar.MenuItems[0]).Text = AppResources.AppBarAbout;
        }
    }
}