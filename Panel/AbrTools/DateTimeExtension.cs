using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AbrBlazorTools
{
    public static class DateTimeExtension
    {

        public static string ComputedDateTime(this DateTime InsertDateTime)
        {
            var persianCalender = new PersianCalendar();
            var year = persianCalender.GetYear(InsertDateTime);
            var month = persianCalender.GetMonth(InsertDateTime);
            var day = persianCalender.GetDayOfMonth(InsertDateTime);
            var hour = persianCalender.GetHour(InsertDateTime);
            var minute = persianCalender.GetMinute(InsertDateTime);
            var second = persianCalender.GetSecond(InsertDateTime);
            var computed = String.Format("{0}-{1}-{2}-{3}{4}{5}", year,
                month < 10 ? String.Format("0{0}", month) : month.ToString(),
                day < 10 ? String.Format("0{0}", day) : day.ToString(),
                hour < 10 ? string.Format("0{0}", hour) : hour.ToString(),
                minute < 10 ? string.Format("0{0}", minute) : minute.ToString(),
                second < 10 ? string.Format("0{0}", second) : second.ToString());
            return computed;

        }
        public static string GetPersianDate(this DateTime value, bool showDayName, bool showMonthName, bool showTime)
        {
            var persianCalender = new PersianCalendar();
            var year = persianCalender.GetYear(value);
            var month = persianCalender.GetMonth(value);
            var day = persianCalender.GetDayOfMonth(value);
            var s = string.Format(showMonthName ? "{2} {1} {0}" : "{0}/{1}/{2}", year, showMonthName ? month.GetMonthName() : month.ToString(""), day);
            if (showDayName)
                s = string.Format("{0} {1}", persianCalender.GetDayOfWeek(value).GetDayName(), s);
            if (showTime)
                s = string.Format("{0} ساعت {1}:{2}", s, value.Hour, value.Minute);
            return s;
        }
        public static string GetPersianDayName(this DateTime value)
        {
            var persianCalender = new PersianCalendar();
            var s = persianCalender.GetDayOfWeek(value).GetDayName();
            return s;
        }
        public static string GetPersianMonthName(this DateTime value)
        {
            var persianCalender = new PersianCalendar();
            var s = persianCalender.GetMonth(value).GetMonthName();
            return s;
        }
        public static int GetPersianDayInt(this DateTime value)
        {
            var persianCalender = new PersianCalendar();
            var s = persianCalender.GetDayOfMonth(value);
            return s;
        }

        public static string GetPersianDate(this DateTime value)
        {
            return value.GetPersianDate(true, true, true);
        }
        public static string GetPersianDate(this DateTime? value)
        {
            return value.HasValue ? value.Value.GetPersianDate(true, true, true) : "";
        }
        public static string GetPersianDate(this DateTime? value, bool showDayName)
        {
            return value.HasValue ? value.Value.GetPersianDate(showDayName, false, false):"";
        }
        public static string GetPersianDate(this DateTime value, bool showDayName)
        {
            return value.GetPersianDate(showDayName, false, false);
        }

        public static string GetPersianDate(this DateTime value, bool showDayName, bool showTime)
        {
            return value.GetPersianDate(showDayName, false, showTime);
        }
        private static string GetMonthName(this int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "امرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default: return "";
            }
        }

        public static string GetDayName(this DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یک شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                default:
                    return "";
            }
        }
    }

    public enum PersianDays
    {
        [Display(Name = "شنبه")]
        Saturday = (int)(DayOfWeek.Saturday),
        [Display(Name = "یکشنبه")]
        Sunday = (int)(DayOfWeek.Sunday),
        [Display(Name = "دوشنبه")]
        Monday = (int)(DayOfWeek.Monday),
        [Display(Name = "سه شنبه")]
        Tuesday = (int)(DayOfWeek.Tuesday),
        [Display(Name = "چهار شنبه")]
        Wednesday = (int)(DayOfWeek.Wednesday),
        [Display(Name = "پنج شنبه")]
        Thursday = (int)(DayOfWeek.Thursday),
        [Display(Name = "چمعه")]
        Friday = (int)(DayOfWeek.Friday),

    }
}