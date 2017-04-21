using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Application.ViewModels.MainHub;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Shell;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class MainPage
    {
        private readonly IList _appBarButtons;
        
        private readonly AppBarButton _graphButton = new AppBarButton
        {
            Text = AppResources.AppBarGraphs,
            IconUri = new Uri("/Themes/Resources/ApplicationBar/Graph.png", UriKind.Relative)
        };
        private readonly AppBarButton _dataButton = new AppBarButton
        {
            Text = AppResources.AppBarData,
            IconUri = new Uri("/Themes/Resources/ApplicationBar/Data.png", UriKind.Relative)
        };

        public MainPage()
        {
            InitializeComponent();
            ApplicationBarInit();
            Loaded += OnLoaded;
            _appBarButtons = new List<object>(ApplicationBar.Buttons.OfType<object>());
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (MainPageViewModel)DataContext;
            _graphButton.Click += OnGraphButtonClick;
            _dataButton.Click += OnDataButtonClick;
            viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void OnDataButtonClick(object sender, EventArgs eventArgs)
        {
            var viewModel = (MainPageViewModel)DataContext;
            viewModel.NavigateToData();
        }

        private void OnGraphButtonClick(object o, EventArgs e)
        {
            var viewModel = (MainPageViewModel)DataContext;
            viewModel.NavigateToGraphs();
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "CanNavigateToGraphs")
            {
                return;
            }
            var viewModel = (MainPageViewModel)DataContext;
            _graphButton.IsEnabled = viewModel.CanNavigateToGraphs;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.New)
            {
                return;
            }

            if (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void ShowOverviewButtons()
        {
            ApplicationBar.Buttons.Insert(0, _graphButton);
            ApplicationBar.Buttons.Insert(0, _dataButton);
        }

        private void RemoveOverviewButtons()
        {
            ApplicationBar.Buttons.Remove(_graphButton);
            ApplicationBar.Buttons.Remove(_dataButton);
        }

        private void OnPanoramaItemsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Items.SelectedItem is OverviewViewModel)
            {
                ShowOverviewButtons();
                return;
            }
            if (Items.SelectedItem is FavoritesViewModel)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Mode = ApplicationBarMode.Minimized;
                return;
            }
            if (ApplicationBar.Mode == ApplicationBarMode.Minimized)
            {
                foreach (var appBarButton in _appBarButtons)
                {
                    ApplicationBar.Buttons.Add(appBarButton);
                }
                
                ApplicationBar.Mode = ApplicationBarMode.Default;
            }
            RemoveOverviewButtons();

        }
        private void ApplicationBarInit()
        {
            ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
            ((AppBarMenuItem) ApplicationBar.MenuItems[0]).Text = AppResources.AppBarLogout;
            ((AppBarMenuItem)ApplicationBar.MenuItems[1]).Text = AppResources.AppBarAbout;
            ((AppBarMenuItem)ApplicationBar.MenuItems[2]).Text = AppResources.AppBarPushSettings;
        }

    }
}