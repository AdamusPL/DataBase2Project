using Microsoft.AspNetCore.Mvc;
using Jsos3.Grades.Builders;
using Jsos3.Grades.Services;

namespace Jsos3.Grades.Controllers;

public class StudentGradeController : Controller
{
    private readonly IGradeIndexViewModelBuilder _gradeIndexVievModelBuilder;
    private readonly IGradeAccepter _gradeAccepter;

    public StudentGradeController(IGradeIndexViewModelBuilder gradeIndexVievModelBuilder, IGradeAccepter gradeAccepter) 
    {
        _gradeIndexVievModelBuilder = gradeIndexVievModelBuilder;
        _gradeAccepter = gradeAccepter;
    }

    public async Task<IActionResult> Index([FromQuery] string? groupId)
    {
        var viewModel = await _gradeIndexVievModelBuilder.Build(groupId);
        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Accept([FromBody] int gradeId)
    {
        await _gradeAccepter.Accept(gradeId);
        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<IActionResult> Decline([FromBody] int gradeId)
    {
        await _gradeAccepter.Decline(gradeId);
        return Json(new { success = true });
    }
}

