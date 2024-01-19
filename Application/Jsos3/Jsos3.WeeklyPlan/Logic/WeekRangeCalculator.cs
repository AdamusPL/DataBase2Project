using Jsos3.WeeklyPlan.Extensions;
using Jsos3.WeeklyPlan.Models;

namespace Jsos3.WeeklyPlan.Logic;

internal interface IWeekRangeCalculator
{
    DateTimeRange Calculate(int? weekOffset);
}

internal class WeekRangeCalculator : IWeekRangeCalculator
{
    private readonly TimeProvider _timeProvider;
    private const int DaysInWeek = 7;
    private const int FridayMondayOffset = 4;

    public WeekRangeCalculator(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public DateTimeRange Calculate(int? weekOffset)
    {
        var now = _timeProvider.GetLocalNow();
        if (weekOffset.HasValue)
        {
            now = now.AddDays(weekOffset.Value * DaysInWeek);
        }

        var startOfWeek = now.StartOfWeek();

        return new()
        {
            Start = startOfWeek,
            End = startOfWeek.AddDays(FridayMondayOffset)
        };
    }
}
