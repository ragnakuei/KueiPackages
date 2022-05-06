using KueiPackages.Models;

namespace KueiPackages.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 增加 offset timeSpan 字串
        /// </summary>
        public static DateTime Add(this DateTime dt, string timeSpan)
        {
            return dt.Add(TimeSpan.Parse(timeSpan));
        }

        /// <summary>
        /// 增加 offset timeSpan 字串
        /// </summary>
        public static DateTime Add(this DateTime dt,
                                   string        timeSpan,
                                   string        format,
                                   CultureInfo?  culture = null)
        {
            return dt.Add(TimeSpan.ParseExact(timeSpan, format, culture ?? CultureInfo.CurrentCulture));
        }
    }
}
