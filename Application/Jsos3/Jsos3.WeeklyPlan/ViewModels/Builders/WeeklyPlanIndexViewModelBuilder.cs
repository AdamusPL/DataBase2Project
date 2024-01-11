using Jsos3.WeeklyPlan.Logic;
using Jsos3.WeeklyPlan.Services;
using Jsos3.WeeklyPlan.ViewModels.Models;

namespace Jsos3.WeeklyPlan.ViewModels.Builders;

public interface IWeeklyPlanIndexViewModelBuilder
{
    Task<WeeklyPlanIndexViewModel> Build(int studentId, int? weekOffset);
}

internal class WeeklyPlanIndexViewModelBuilder : IWeeklyPlanIndexViewModelBuilder
{
    private readonly IPlanService _planService;
    private readonly IWeekRangeCalculator _weekRangeCalculator;

    public WeeklyPlanIndexViewModelBuilder(IPlanService planService, IWeekRangeCalculator weekRangeCalculator)
    {
        _planService = planService;
        _weekRangeCalculator = weekRangeCalculator;
    }

    public async Task<WeeklyPlanIndexViewModel> Build(int studentId, int? weekOffset)
    {
        var range = _weekRangeCalculator.Calculate(weekOffset);
        var plan = await _planService.GetStudentPlan(studentId, range.Start, range.End);

        return new()
        {
            StartOfWeek = range.Start,
            EndOfWeek = range.End,
            Occurences = plan
        };
    }
}
