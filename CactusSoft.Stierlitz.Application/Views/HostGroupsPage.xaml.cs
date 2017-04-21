using System.Linq;
using System.Windows.Controls;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class HostGroupsPage
    {
        public HostGroupsPage()
        {
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, HostGroupsRadDataBoundListBox);
		}

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarGraphs;
            ((AppBarButton)ApplicationBar.Buttons[1]).Text = AppResources.AppBarRefresh;
        }

	}
}