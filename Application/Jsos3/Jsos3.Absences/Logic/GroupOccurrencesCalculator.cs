using System;
using System.Globalization;
using Jsos3.Shared.Models;

public interface IGroupOccurrencesCalculator
{
    List<DateTime> Calculate(DateTime start, DateTime end, DayOfWeek dayOfWeek, Regularity regularity);
}

internal class GroupOccurrencesCalculator : IGroupOccurrencesCalculator
{
    public List<DateTime> Calculate(DateTime start, DateTime end, DayOfWeek dayOfWeek, Regularity regularity)
    {
        var days = new List<DateTime>();

        for (var date = start; date <= end; date = date.AddDays(1))
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