using System.Windows;
using CactusSoft.Stierlitz.Common;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public class GlobalBusyIndicatorManager : DependencyObject, IGlobalBusyIndicatorManager
    {
        public static readonly DependencyProperty BusyIndicatorProperty =
            DependencyProperty.RegisterAttached("BusyIndicator", typeof (IGlobalBusyIndicatorManager),
                                                typeof (GlobalBusyIndicatorManager), new PropertyMetadata(null));


        private static ProgressIndicator _progressIndicator;
        private readonly PhoneApplicationPage _page;
        private int _isBusyCounter;

        public static IGlobalBusyIndicatorManager Create(PhoneApplicationPage page)
        {
            var busyIndicator = page.GetValue(BusyIndicatorProperty) as IGlobalBusyIndicatorManager;
            if (busyIndicator == null)
            {
                busyIndicator = new GlobalBusyIndicatorManager(page);
                page.SetValue(BusyIndicatorProperty, busyIndicator);
            }
            
            return busyIndicator;
        }

        private GlobalBusyIndicatorManager(PhoneApplicationPage page)
        {
            _page = page;

            if (_progressIndicator == null)
            {
                _progressIndicator = new ProgressIndicator();
            }

            _page.SetValue(SystemTray.ProgressIndicatorProperty, _progressIndicator);
        }

        public bool IsBusy
        {
            get
            {
                return _isBusyCounter > 0;
            }

            set
            {
                if (value)
                {
                    _isBusyCounter++;
                }
                else if (_isBusyCounter > 0)
                {
                    _isBusyCounter--;
                }

                UpdateIndicatorVisibility();
            }
        }

        private void UpdateIndicatorVisibility()
        {
            var isRunning = _isBusyCounter > 0;

            Dispatcher.BeginInvoke(
                () =>
                    {
                        _progressIndicator.IsVisible = _progressIndicator.IsIndeterminate = isRunning;
                    });
        }
    }
}