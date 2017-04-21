using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class HostsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hosts = (IEnumerable<Host>)value ?? new List<Host>();
            return string.Join(", ", hosts.Select(h => h.Name));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
