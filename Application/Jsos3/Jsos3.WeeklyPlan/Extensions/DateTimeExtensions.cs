namespace Jsos3.WeeklyPlan.Extensions;

internal static class DateTimeExtensions
{
    internal static DateTime StartOfWeek(this DateTimeOffset dt)
    {
        int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}
