using Jsos3.Shared.Auth;
using Jsos3.WeeklyPlan.Logic;
using Jsos3.WeeklyPlan.Services;
using Jsos3.WeeklyPlan.ViewModels.Models;

namespace Jsos3.WeeklyPlan.ViewModels.Builders;

public interface IWeeklyPlanIndexViewModelBuilder
{
    Task<WeeklyPlanIndexViewModel> Build(int userObjectId, UserType userType, int? weekOffset);
}

internal class WeeklyPlanIndexViewModelBuilder : IWeeklyPlanIndexViewModelBuilder
{
    private readonly IPlanService _planService;
    private readonly IWeekRangeCalculator _weekRangeCalculator;
    private readonly IWeekDataCalculator _weekDataCalculator;

    public WeeklyPlanIndexViewModelBuilder(IPlanService planService, IWeekRangeCalculator weekRangeCalculator, IWeekDataCalculator weekDataCalculator)
    {
        _planService = planService;
        _weekRangeCalculator = weekRangeCalculator;
        _weekDataCalculator = weekDataCalculator;
    }

    public async Task<WeeklyPlanIndexViewModel> Build(int userObjectId, UserType userType, int? weekOffset)
    {
        var range = _weekRangeCalculator.Calculate(weekOffset);
        var plan = await _planService.GetPlanForUser(userObjectId, userType, range.Start, range.End);
        var weekData = _weekDataCalculator.Calculate(range);

        return new()
        {
            Week = weekData,
            Occurences = plan
        };
    }
}
