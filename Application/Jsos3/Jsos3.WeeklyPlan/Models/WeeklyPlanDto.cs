using Jsos3.Shared.Models;

namespace Jsos3.WeeklyPlan.Models;

public class WeeklyPlanDto
{
    public required DateTime Date { get; set; }
    public required string Regularity { get; set; }
    public required string Course { get; set; }
    public required string GroupType { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required string Classroom { get; set; }
    public required string Lecturer { get; set; }
}
