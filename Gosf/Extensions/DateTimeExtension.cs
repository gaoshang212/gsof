using System;

namespace Gsof.Shared.Extensions
{
    /// <summary>
    /// DateTime 扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// DateTime 转换为毫秒时间戳
        /// </summary>
        /// <param name="p_dateTime">DateTime</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime p_dateTime)
        {
            return (long)(p_dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }

        /// <summary>
        /// 毫秒时间戳转换为 DateTime
        /// </summary>
        /// <param name="p_timestamp">long 时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long p_timestamp)
        {
            var ts = TimeSpan.FromMilliseconds(p_timestamp);
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).Add(ts);
        }
    }
}
