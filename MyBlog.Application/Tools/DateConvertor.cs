using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Tools
{
    public static class DateConvertor
    {
        public static string ConvertToShamsi( this DateTime value)
        {
            PersianCalendar calender = new PersianCalendar();

            return calender.GetYear(value) + "/" + calender.GetMonth(value).ToString("00") + "/" + calender.GetDayOfMonth(value).ToString("00");
        }
    }
}
