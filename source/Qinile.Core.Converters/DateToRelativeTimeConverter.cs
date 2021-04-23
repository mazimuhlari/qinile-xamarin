using System;
using Xamarin.Forms;

namespace Qinile.Core.Converters
{
    public class DateToRelativeTimeConverter : IValueConverter
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

            if (delta < 1 * MINUTE)
                return span.Seconds == 1 ? "one second ago" : span.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return span.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return span.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return span.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = System.Convert.ToInt32(Math.Floor((double)span.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = System.Convert.ToInt32(Math.Floor((double)span.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}