using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls.SlideView;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class GraphPage : PhoneApplicationPage
    {
        private readonly AppBarButton _subscribeButton;
        public GraphPage()
        {
            InitializeComponent();
            ApplicationBarInit();
            _subscribeButton = (AppBarButton) ApplicationBar.Buttons[2];
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (GraphPageViewModel)DataContext;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            ChangeSubscribeButtonState(viewModel.IsSubscribed);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsSubscribed")
            {
                var viewModel = (GraphPageViewModel)DataContext;
                ChangeSubscribeButtonState(viewModel.IsSubscribed);
            }
            else if (propertyChangedEventArgs.PropertyName == "IsFirstLoading")
            {
                var viewModel = (GraphPageViewModel)DataContext;
                if (!viewModel.IsFirstLoading)
                {
                    ProgressContainer.VerticalAlignment = VerticalAlignment.Top;
                }
            }
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

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            if (e.Orientation == PageOrientation.LandscapeRight)
            {
                GraphImage.Margin = new Thickness(90, 0, 0, 0);
                GraphImage.ZoomMode = ImageZoomMode.Free;
                GraphImage.Width = 710;
                GraphImage.Height = 480;
                Scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                ProgressBar.Width = 800;
            }
            else if(e.Orientation == PageOrientation.LandscapeLeft)
            {
                GraphImage.Margin = new Thickness(0, 0, 90, 0);
                GraphImage.ZoomMode = ImageZoomMode.Free;
                GraphImage.Width = 710;
                GraphImage.Height = 480;
                Scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                ProgressBar.Width = 800;
            }
            else if(e.Orientation == PageOrientation.PortraitUp)
            {
                GraphImage.Margin = new Thickness(0, 0, 0, 72);
                GraphImage.ZoomMode = ImageZoomMode.None;
                GraphImage.Width = 1076;
                GraphImage.Height = 728;
                Scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                ProgressBar.Width = 480;
            }
        }

        private void ApplicationBarInit()
        {
            ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarPrevious;
            ((AppBarButton) ApplicationBar.Buttons[1]).Text = AppResources.AppBarNext;
            ((AppBarButton) ApplicationBar.Buttons[2]).Text = AppResources.AppBarSubscribe;
            ((AppBarMenuItem) ApplicationBar.MenuItems[0]).Text = AppResources.AppBarPeriod;
        }
    }
}