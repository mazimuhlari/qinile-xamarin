using System;
using Xamarin.Forms;

namespace Qinile.Core.Converters
{
    public class DateOfBirthToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null)
                return 0;

            var dateOfBirth = (DateTime)value;
            var today = DateTime.Today;

            var age = today.Year - dateOfBirth.Year;

            return age;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}