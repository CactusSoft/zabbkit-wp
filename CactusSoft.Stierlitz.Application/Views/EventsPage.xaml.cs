using System;
using System.ComponentModel;
using System.Windows;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class EventsPage
    {
        private readonly AppBarButton _subscribeButton;
        public EventsPage()
		{
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, EventsRadDataBoundListBox);
            _subscribeButton = (AppBarButton) ApplicationBar.Buttons[1];
            Loaded += OnLoaded;
		}

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (EventsPageViewModel) DataContext;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            ChangeSubscribeButtonState(viewModel.IsSubscribed);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "IsSubscribed")
            {
                return;
            }
            var viewModel = (EventsPageViewModel)DataContext;
            ChangeSubscribeButtonState(viewModel.IsSubscribed);
        }

        private void ChangeSubscribeButtonState(bool isSubscibed)
        {
            if (!isSubscibed)
            {
                _subscribeButton.IconUri = new Uri("/Themes/Resources/ApplicationBar/appbar.subscribe.png", UriKind.Relative);
                _subscribeButton.Text = AppResources.AppBarSubscribe;
            }
            else
            {
                _subscribeButton.IconUri = new Uri("/Themes/Resources/ApplicationBar/appbar.unsubscribe.png", UriKind.Relative);
                _subscribeButton.Text = AppResources.AppBarUnsubscribe;
            }
        }

        private void OnSelectionChanging(object sender, SelectionChangingEventArgs e)
        {
            e.Cancel = true;
        }

        private void ApplicationBarInit()
        {
            ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
            ((AppBarButton) ApplicationBar.Buttons[1]).Text = AppResources.AppBarSubscribe;
        }

    }
}