using System;
using System.Globalization;
using System.Windows.Data;

namespace _2048
{
    public class BoardDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? number = (int?)value;

            if (number == null)
                return null;

            return (int)Math.Pow(2, number.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}