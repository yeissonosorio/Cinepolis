using System;
using System.Globalization;

namespace Cinepolis.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64Image)
            {
                byte[] imageAsBytes = System.Convert.FromBase64String(base64Image);
                return ImageSource.FromStream(() => new System.IO.MemoryStream(imageAsBytes));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}