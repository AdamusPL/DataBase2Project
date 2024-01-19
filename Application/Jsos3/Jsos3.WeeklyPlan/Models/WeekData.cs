namespace Jsos3.WeeklyPlan.Models;

public class WeekData
{
    public required DateTime StartOfWeek { get; set; }
    public required DateTime EndOfWeek { get; set; }
    public required TimeSpan TimeGap { get; set; }
    public required List<DateTime> Days { get; set; }
    public required List<TimeSpan> Hours { get; set; }
}
