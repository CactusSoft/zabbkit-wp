using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CactusSoft.Stierlitz.Application.ViewModels;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class DataPage
    {
        public DataPage()
        {
            InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, ItemsRadDataBoundListBox);
        }

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
        }
    }
}