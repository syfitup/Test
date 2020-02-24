using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Framework
{
    public static class DateExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-diff).Date;
        }
    }
}