using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class TriggerToColorConverter : IValueConverter
    {
        private static readonly Brush DefaultBrush = new SolidColorBrush(Color.FromArgb(0xFF, 75, 225, 75));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trigger = (Trigger)value;

            if (trigger.IsOk)
            {
                return DefaultBrush;
            }

            return trigger.Priority.ToBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}