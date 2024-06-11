using System.Text.Json.Serialization;
using System.Text.Json;

namespace Dima.Core.Common.Extensions;

//public static class DateTimeExtension
//{
//    public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null)
//        => new(year ?? date.Year, month ?? date.Month, 1);

//    public static DateTime GetLastDay(
//        this DateTime date, int? year = null, int? month = null)
//        => new DateTime(
//                year ?? date.Year,
//                month ?? date.Month,
//                1)
//            .AddMonths(1)
//            .AddDays(-1);
//}



public static class DateTimeExtension
{
    public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null)
    {
        var firstDay = new DateTime(year ?? date.Year, month ?? date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        return firstDay;
    }

    public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
    {
        var lastDay = new DateTime(year ?? date.Year, month ?? date.Month, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMonths(1)
            .AddDays(-1);
        return lastDay;
    }
}
