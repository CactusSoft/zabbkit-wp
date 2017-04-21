using CactusSoft.Stierlitz.Localization;
using Caliburn.Micro;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views
{
    public partial class TimelinePage
    {
        public TimelinePage()
		{
			InitializeComponent();
            ApplicationBarInit();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, EventsRadDataBoundListBox);
		}

        private void ApplicationBarInit()
        {
            ((AppBarButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarRefresh;
        }

    }
}