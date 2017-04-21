using System;
using System.Globalization;
using System.Windows.Data;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class StringToStringWithColonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Concat((string)value, ":");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
