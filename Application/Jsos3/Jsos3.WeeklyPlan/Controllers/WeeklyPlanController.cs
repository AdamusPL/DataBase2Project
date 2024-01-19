using Jsos3.Shared.Auth;
using Jsos3.WeeklyPlan.ViewModels.Builders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.WeeklyPlan.Controllers;

public class WeeklyPlanController : Controller
{
    private readonly IWeeklyPlanIndexViewModelBuilder _weeklyPlanIndexViewModelBuilder;
    private readonly IUserAccessor _userAccessor;

    public WeeklyPlanController(IWeeklyPlanIndexViewModelBuilder weeklyPlanIndexViewModelBuilder, IUserAccessor userAccessor)
    {
        _weeklyPlanIndexViewModelBuilder = weeklyPlanIndexViewModelBuilder;
        _userAccessor = userAccessor;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public async Task<IActionResult> Index([FromQuery] int? weekOffset)
    {
        var viewModel = await _weeklyPlanIndexViewModelBuilder.Build(
            _userAccessor.Id,
            _userAccessor.Type,
            weekOffset);
        return View(viewModel);
    }
}
