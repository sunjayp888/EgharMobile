using System;
using System.Collections.Generic;

namespace Egharpay.Business.Extensions
{
    public static class DateTimeExtensions
    {
        public static IEnumerable<DateTime> RangeTo(this DateTime from, DateTime to, Func<DateTime, DateTime> step = null)
        {
            if (step == null)
                step = x => x.AddDays(1);

            while (from <= to)
            {
                yield return from;
                from = step(from);
            }
        }

        public static IEnumerable<DateTime> RangeFrom(this DateTime to, DateTime from, Func<DateTime, DateTime> step = null)
        {
            return from.RangeTo(to, step);
        }

        public static string FiscalYear(this DateTime now)
        {
            return string.Concat(now.Year, "-", now.AddYears(1).Year);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
        public static string Formatted(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string Formatted(this DateTime? dateTime)
        {
            return dateTime.HasValue == false ? "" : dateTime.Value.Formatted();
        }

        public static DateTime? CombineDateTime(this DateTime? dateTime, string time)
        {
            var timeSplit = time.Split(':');
            if (!dateTime.HasValue || string.IsNullOrEmpty(time))
                return null;
            var hour = Convert.ToInt32(timeSplit[0]);
            var minute = Convert.ToInt32(timeSplit[1]);
            var date = new DateTime(dateTime.Value.Year,
                                    dateTime.Value.Month,
                                    dateTime.Value.Day,
                                    hour, minute, 0);
            return date;
        }

        public static bool IsBetween(this DateTime date, TimeSpan start, TimeSpan end)
        {
            
            var time = date.TimeOfDay;
            // If the start time and the end time is in the same day.
            if (start <= end)
                return time >= start && time <= end;
            // The start time and end time is on different days.
            return time >= start || time <= end;
        }
    }
}