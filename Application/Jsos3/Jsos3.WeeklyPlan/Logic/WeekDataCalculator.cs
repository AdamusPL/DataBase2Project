using Jsos3.WeeklyPlan.Models;

namespace Jsos3.WeeklyPlan.Logic;

internal interface IWeekDataCalculator
{
    WeekData Calculate(DateTimeRange range);
}

internal class WeekDataCalculator : IWeekDataCalculator
{
    private readonly static TimeSpan StartOfDay = new(8, 0, 0);
    private readonly static TimeSpan TimeGap = new(0, 30, 0);
    private readonly static TimeSpan EndOfDay = new(20, 0, 0);

    public WeekData Calculate(DateTimeRange range) =>
        new()
        {
            StartOfWeek = range.Start,
            EndOfWeek = range.End,
            Days = GetDaysOfWeek(range),
            Hours = GetDayHours(),
            TimeGap = TimeGap
        };

    private List<DateTime> GetDaysOfWeek(DateTimeRange range)
    {
        var days = new List<DateTime>();

        for (var date = range.Start; date <= range.End; date = date.AddDays(1))
        {
            days.Add(date);
        };

        return days;
    }

    private List<TimeSpan> GetDayHours()
    {
        var hours = new List<TimeSpan>();

        for (var time = StartOfDay; time <= EndOfDay; time = time.Add(TimeGap))
        {
            hours.Add(time);
        };

        return hours;
    }
}
