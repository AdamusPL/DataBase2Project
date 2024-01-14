using Jsos3.WeeklyPlan.Models;

namespace Jsos3.WeeklyPlan.ViewModels.Models;

public class WeeklyPlanIndexViewModel
{
    public required WeekData Week { get; set; }
    public required Dictionary<DateTime, List<WeeklyPlanDto>> Occurences { get; set; }
}