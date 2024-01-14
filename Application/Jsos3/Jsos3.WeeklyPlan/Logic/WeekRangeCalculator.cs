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

    public WeekRangeCalculator(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public DateTimeRange Calculate(int? weekOffset)
    {
        var now = _timeProvider.GetLocalNow();
        if (weekOffset.HasValue)
        {
            now = now.AddDays(weekOffset.Value * 7);
        }

        var startOfWeek = now.StartOfWeek();

        return new()
        {
            Start = startOfWeek,
            End = startOfWeek.AddDays(6)
        };
    }
}
