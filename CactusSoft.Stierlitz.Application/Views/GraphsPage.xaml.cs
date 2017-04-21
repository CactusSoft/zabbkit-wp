using System.Windows.Controls;
using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class GraphsPage : PhoneApplicationPage
    {
        public GraphsPage()
		{
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, GraphsRadDataBoundListBox);
		}

        private void ApplicationBarInit()
        {
            ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
        }
    }
}