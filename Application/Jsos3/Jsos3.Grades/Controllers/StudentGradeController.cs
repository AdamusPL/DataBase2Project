using Microsoft.AspNetCore.Mvc;
using Jsos3.Grades.Builders;

namespace Jsos3.Grades.Controllers;

public class StudentGradeController : Controller
{
    private readonly IGradeIndexViewModelBuilder _gradeIndexVievModelBuilder;

    public StudentGradeController(IGradeIndexViewModelBuilder gradeIndexVievModelBuilder) 
    {
        _gradeIndexVievModelBuilder = gradeIndexVievModelBuilder;
    }

    public async Task<IActionResult> Index([FromQuery] string? groupId)
    {
        var viewModel = await _gradeIndexVievModelBuilder.Build(groupId);
        return View(viewModel);
    }
}
