using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace CactusSoft.Stierlitz.Application.Views.Period
{
    public partial class PeriodPage : PhoneApplicationPage
    {
        public PeriodPage()
        {
            InitializeComponent();
            SetValue(RadTileAnimation.ContainerToAnimateProperty, EventsRadDataBoundListBox);
        }

        protected override void OnOrientationChanged(Microsoft.Phone.Controls.OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            if (e.Orientation == PageOrientation.LandscapeLeft)
            {
                ContentLayout.Margin = new Thickness(72, 24, 24, 0);
            }
            else if (e.Orientation == PageOrientation.LandscapeRight)
            {
                ContentLayout.Margin = new Thickness(72, 24, 24, 0);
            }
            else if (e.Orientation == PageOrientation.PortraitUp)
            {
                ContentLayout.Margin = new Thickness(0, 32, 0, 0);
            }
        }
    }
}