using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
	public partial class AboutPage : PhoneApplicationPage
	{
	    public AboutPage()
	    {
	        InitializeComponent();

	        ApplicationBarInit();
	    }

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.Email;

        }
	}
}