
using System.Globalization;

namespace Backend.Shared;

public record DateAndTime(string Date, string Time);

/// <summary>
/// This class represent  converter methods for converting from DateTime UtcNow to Persian Calender and vise versa.
/// </summary>
public class DateTimeConverter
{
    /// <summary>
    /// Convert To Persian Calender
    /// </summary>
    /// <param name="dateTime">Represent DateTime UtcNow</param>
    /// <returns>string with this format year/month/day. time: [hour:minute]</returns>





    public DateAndTime ConvertToPersianCalender(DateTime dateTime)
    {
        /* warning: the ("Iran Standard Time") only works for windows systems
         for linux use ("Asia/Tehran") */
        TimeZoneInfo iranZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        DateTime iranTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, iranZone);
        var calender = new PersianCalendar();
        var year = calender.GetYear(iranTime);
        var month = calender.GetMonth(iranTime);
        var day = calender.GetDayOfMonth(iranTime);
        var hour = calender.GetHour(iranTime);
        var minute = calender.GetMinute(iranTime);


        return new DateAndTime(
            Date: $"{year}/{month}/{day}",
            Time: $"{hour}:{minute}");
    }

}
