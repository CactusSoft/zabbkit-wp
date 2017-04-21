using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
	public partial class ServersPage : PhoneApplicationPage
	{
	    public ServersPage()
	    {
	        InitializeComponent();
	        ApplicationBarInit();
	    }

	    private void ApplicationBarInit()
	    {
	        ((AppBarButton) ApplicationBar.Buttons[0]).Text = AppResources.AppBarSave;
            ((AppBarButton) ApplicationBar.Buttons[1]).Text = AppResources.AppBarCancel;
	    }
	}
}