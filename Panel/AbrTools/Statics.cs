using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infrastructure;
using Models.Config;
using Models.Identity;

namespace AbrBlazorTools
{
    public static class Statics
    {
        public static string DefaultOrganization = "nidc";
        public static string OrganizationUrl { set; get; }
        public static string PageTitle { set; get; } = "";
        public static ConfigDetails ConfigDetails { set; get; } = new ConfigDetails();
        public static string LoginTokenKey = "SignIn";
        public static string RefreshTokenKey = "RefreshToken";
        public static DateTime? LastCheck { set; get; }

        public static CheckLoginResponse CurrentLoginInfo { get; set; }
        public static string AppDomain { get; set; } = "wsim-app";

        public static string TableClass = "table table-striped table-bordered border";

        //public static string BaseAddress = "https://hafari.paanaak.net/";
        public static string BaseAddress => Consts.BaseAddress;
        public static string ReplaceNewLineWithBr(this string text)
        {
            var result = Regex.Replace(text, @"\r\n?|\n", "<br/>");
            result = result.Replace("<br/><br/><br/>", "<br/>").Replace("<br/><br/>", "<br/>");
            return result;
        }
        public static CultureInfo GetPersianCulture()
        {
            var culture = new CultureInfo("fa-IR");
            DateTimeFormatInfo formatInfo = culture.DateTimeFormat;
            formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
            var monthNames = new[]
            {
                "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                "اسفند",
                "",
            };
            formatInfo.AbbreviatedMonthNames =
                formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
            formatInfo.AMDesignator = "ق.ظ";
            formatInfo.PMDesignator = "ب.ظ";
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
            System.Globalization.Calendar cal = new PersianCalendar();
            FieldInfo fieldInfo = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
                fieldInfo.SetValue(culture, cal);
            FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (info != null)
                info.SetValue(formatInfo, cal);
            culture.NumberFormat.NumberDecimalSeparator = "/";
            culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            culture.NumberFormat.NumberNegativePattern = 0;
            return culture;
        }
        public static string GetDisplayName<T>(string propertyname, object tIte)
        {
            MemberInfo myprop = typeof(T).GetProperty(propertyname) as MemberInfo;
            var dd = myprop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            var resourceType = dd.ResourceType;
            var resourceKey = dd.Name;

            if (resourceType is null) return resourceKey ?? "";

            var resourceManagerMethodInfo = resourceType.GetProperty(nameof(ResourceManager), BindingFlags.Public | BindingFlags.Static);

            var resourceManager = (ResourceManager)resourceManagerMethodInfo?.GetValue(null);

            return resourceManager?.GetString(resourceKey, CultureInfo.CurrentUICulture);

        }
    }
}
