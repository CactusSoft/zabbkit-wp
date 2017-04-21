using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CactusSoft.Stierlitz.Application.Converters
{
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rawImageBytes = (byte[])value;
            BitmapImage imageSource = null;

            try
            {
                using (var stream = new MemoryStream(rawImageBytes))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var b = new BitmapImage();
                    b.SetSource(stream);
                    imageSource = b;
                }
            }
            catch (Exception)
            {
            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
