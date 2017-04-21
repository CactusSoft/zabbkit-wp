using System.Windows;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public static class ThemeHelper
    {
        public static bool IsDarkTheme
        {
            get
            {
                return (Visibility) System.Windows.Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible;
            }
        }
    }
}
