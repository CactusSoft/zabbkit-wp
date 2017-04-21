using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
	public partial class ServerPickerPage : PhoneApplicationPage
	{
        public ServerPickerPage()
		{
			InitializeComponent();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, ServersRadDataBoundListBox);
            ApplicationBarInit();
		}

	    private void ApplicationBarInit()
	    {
	        ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarAdd;
	    }
	}
}