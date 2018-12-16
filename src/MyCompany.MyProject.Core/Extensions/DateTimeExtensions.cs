using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    public static class DateTimeExtensions
    {
        public static DateTime NextDayDate(this DateTime dateTime)
        {
            return dateTime.AddDays(1).Date;
        }
    }
}
