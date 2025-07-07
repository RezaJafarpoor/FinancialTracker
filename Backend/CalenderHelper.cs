using System.Globalization;

namespace Backend;

public class CalenderHelper
{
    public string ConvertToPersian()
    {
        var date = DateTime.UtcNow;
        var persianDate = convert(date);
        return persianDate;
    }



    private string convert(DateTime dateTime)
    {
        /* warning: the ("Iran Standard Time") only works for windows systems
         for linux use ("Asia/Tehran") */
        TimeZoneInfo iranZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        DateTime iranTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, iranZone);
        var calender = new PersianCalendar();
        var year = calender.GetYear(iranTime);
        var month = calender.GetMonth(iranTime);
        var day = calender.GetDayOfMonth(iranTime);
        return $"{year}/{month}/{day}";

    }

}
