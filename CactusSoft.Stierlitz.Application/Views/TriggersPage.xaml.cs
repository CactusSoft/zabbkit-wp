using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class TriggersPage : PhoneApplicationPage
    {
        public TriggersPage()
		{
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, TriggersRadDataBoundListBox);
		}

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
        }

    }
}