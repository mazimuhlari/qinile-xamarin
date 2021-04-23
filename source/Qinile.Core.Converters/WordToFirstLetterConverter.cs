using System;
using Xamarin.Forms;

namespace Qinile.Core.Converters
{
    public class WordToFirstLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var word = (string)value;

            if (string.IsNullOrWhiteSpace(word))
                return "";

            return word.Substring(0, 1);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
