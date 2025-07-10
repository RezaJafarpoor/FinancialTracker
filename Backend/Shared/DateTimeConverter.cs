
using System.Globalization;

namespace Backend.Shared;

public record DateAndTime(string Date, string Time);

/// <summary>
/// This class represent  converter methods for converting from DateTime UtcNow to Persian Calender and vise versa.
/// </summary>
public class DateTimeConverter
{
    /// <summary>
    /// Convert from DateTimeUtc to Persian Calender
    /// </summary>
    /// <param name="dateTime">Represent DateTime UtcNow</param>
    /// <returns>A <c>DateAndTime</c> object represent date and time of the day</returns>
    public DateAndTime ConvertFromUtcToPersianCalender(DateTime dateTime)
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

    /// <summary>
    /// Convert from persian calender to DateTimeUtc
    /// </summary>
    /// <param name="year">year in persian calender</param>
    /// <param name="month">month in persian calender</param>
    /// <param name="day">day in persian calender</param>
    /// <param name="hour">hour </param>
    /// <param name="minute">minute </param>
    /// <returns> A <c>DateTime</c> object represent  Utc time</returns>
    public DateTime ConvertFromPersianCalenderToUtc(int year, int month, int day, int hour, int minute)
    {
        var calender = new PersianCalendar();
        var userDateTime = calender.ToDateTime(year, month, day, hour, minute, 0, 0, 0);
        var dateTime = TimeZoneInfo.ConvertTimeToUtc(userDateTime);
        return dateTime;
    }
}
