using System;
using System.Windows;
using System.Windows.Media.Animation;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class HostTriggersPage : PhoneApplicationPage
    {
        public HostTriggersPage()
		{
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, TriggersRadDataBoundListBox);
		}

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarData;
            ((AppBarButton)ApplicationBar.Buttons[1]).Text = AppResources.AppBarGraphs;
            ((AppBarButton)ApplicationBar.Buttons[2]).Text = AppResources.AppBarRefresh;
        }

    }
}