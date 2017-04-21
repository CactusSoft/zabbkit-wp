using System;
using System.Windows;
using CactusSoft.Stierlitz.Application.Helpers;

namespace CactusSoft.Stierlitz.Application
{
	public partial class App
	{
		public App()
		{
			InitializeComponent();
		    ChangeColorTheme();
			EnableDiagnostics();
		}

	    private void ChangeColorTheme()
        {
            if (!ThemeHelper.IsDarkTheme)
            {
                Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/CactusSoft.Stierlitz.Application;component/Themes/Styles.Light.xaml", UriKind.Relative) });
            }
        }

		[System.Diagnostics.Conditional("DEBUG")]
		private static void EnableDiagnostics()
		{
			if (!System.Diagnostics.Debugger.IsAttached)
			{
				return;
			}

			Current.Host.Settings.EnableFrameRateCounter = true;
			MetroGridHelper.IsVisible = true;
		}

        
	}
}