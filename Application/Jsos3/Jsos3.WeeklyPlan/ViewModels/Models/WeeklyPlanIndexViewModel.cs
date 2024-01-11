using Jsos3.WeeklyPlan.Models;

namespace Jsos3.WeeklyPlan.ViewModels.Models;

public class WeeklyPlanIndexViewModel
{
    public required DateTime StartOfWeek { get; set; }
    public required DateTime EndOfWeek { get; set; }
    public required Dictionary<DateTime, List<WeeklyPlanDto>> Occurences { get; set; }
}