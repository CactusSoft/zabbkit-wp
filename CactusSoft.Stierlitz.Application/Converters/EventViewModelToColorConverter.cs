using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CactusSoft.Stierlitz.Application.ViewModels;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class EventViewModelToColorConverter : IValueConverter
    {
        private static readonly Brush DefaultBrush = new SolidColorBrush(Color.FromArgb(0xFF, 75, 225, 75));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventViewModel = (EventViewModel)value;

            if (eventViewModel.IsOk)
            {
                return DefaultBrush;
            }

            return eventViewModel.TriggerPriority.ToBrush();           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
