using System;
using System.Globalization;

internal enum Regularity
{
    Odd = 1,
    Even = 2,
    Every = 3
}

internal interface IGroupOccurencesCalculator
{
    List<DateTime> Calculate(DateTime start, DateTime end, DayOfWeek dayOfWeek, int regularityId);
}

internal class GroupOccurencesCalculator : IGroupOccurencesCalculator
{
    public List<DateTime> Calculate(DateTime start, DateTime end, DayOfWeek dayOfWeek, int regularityId)
    {
       
        Regularity regularity = (Regularity)Enum.Parse(typeof(Regularity), regularityId.ToString());
        var days = new List<DateTime>();

        for (var date = start; date <= end; date.AddDays(1))
        {
            if (date.DayOfWeek != dayOfWeek)
            {
                continue;
            }

            if (regularity == Regularity.Every)
            {
                days.Add(date);
                continue;
            }

            var isEven = IsWeekEven(date);

            if (regularity == Regularity.Even && isEven
                || regularity == Regularity.Odd && !isEven)
            {
                days.Add(date);
            }
        }

        return days;
    }

    private bool IsWeekEven(DateTime date)
    {
        var cal = DateTimeFormatInfo.CurrentInfo.Calendar;
        int weekOfYear = cal.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        return weekOfYear % 2 == 0;
    }
}