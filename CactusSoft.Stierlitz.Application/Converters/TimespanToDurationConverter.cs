using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using CactusSoft.Stierlitz.Localization;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class TimespanToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timespan = (TimeSpan) value;
            var durationBuilder = new StringBuilder();
            
            if (timespan.Days > 0)
            {
                durationBuilder.Append(string.Format("{0}{1} ", timespan.Days, AppResources.ShortDay));
            }
            if (timespan.Hours > 0 || durationBuilder.Length > 0)
            {
                durationBuilder.Append(string.Format("{0}{1} ", timespan.Hours, AppResources.ShortHour));
            }
            if (timespan.Minutes > 0 || durationBuilder.Length > 0)
            {
                durationBuilder.Append(string.Format("{0}{1}", timespan.Minutes, AppResources.ShortMinute));
            }
            if (timespan.Minutes == 0 && durationBuilder.Length == 0)
            {
                durationBuilder.Append(string.Format("{0}{1}", timespan.Seconds, AppResources.ShortSecond));
            }
            return durationBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
