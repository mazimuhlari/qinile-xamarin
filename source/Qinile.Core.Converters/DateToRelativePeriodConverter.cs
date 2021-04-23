using System;
using Xamarin.Forms;

namespace Qinile.Core.Converters
{
    public class DateToRelativePeriodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var date = (DateTime)value;

            if (date == null)
                return null;

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var span = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            double delta = Math.Abs(span.TotalSeconds);

            if (delta < 24 * HOUR)
                return date.ToString("HH:mm");

            if (delta < 48 * HOUR)
                return "Yesterday";

            if (delta < 7 * DAY)
                return date.ToString("ddd");

            if (delta < 12 * MONTH)
                return date.ToString("yyyy/MM/dd");
            else
                return date.ToString("yyyy/MM/dd");


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}