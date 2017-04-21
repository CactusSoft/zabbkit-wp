using System.Windows;
using CactusSoft.Stierlitz.Localization;
using Microsoft.Phone.Controls;

namespace CactusSoft.Stierlitz.Application.Views.Period
{
    public partial class CustomPeriodPage
    {
        public CustomPeriodPage()
        {
            InitializeComponent();
            TimeSpanPicker.DisplayValueFormat = string.Format("dd'{0}'' 'h'{1}'", AppResources.ShortDay, AppResources.ShortHour);
        }

        protected override void OnOrientationChanged(Microsoft.Phone.Controls.OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            if (e.Orientation == PageOrientation.LandscapeLeft)
            {
                ContentLayout.Margin = new Thickness(72, 24, 104, 0);
            }
            else if (e.Orientation == PageOrientation.LandscapeRight)
            {
                ContentLayout.Margin = new Thickness(72, 24, 104, 0);
            }
            else if (e.Orientation == PageOrientation.PortraitUp)
            {
                ContentLayout.Margin = new Thickness(0, 32, 0, 0);
            }
        }
    }
}