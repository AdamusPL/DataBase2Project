

using Jsos3.Grades.Builders;
using Jsos3.Grades.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Grades.Controllers;

internal class LecturerGradeController : Controller
{
    private readonly ILecturerGradeIndexViewModelBuilder _lecturerGradeIndexViewModelBuilder;
    public LecturerGradeController(ILecturerGradeIndexViewModelBuilder lecturerGradeIndexViewModelBuilder)
    {
        _lecturerGradeIndexViewModelBuilder = lecturerGradeIndexViewModelBuilder;

    }

    public async Task<IActionResult> Index([FromQuery] string groupId)
    {
        var viewModel = await _lecturerGradeIndexViewModelBuilder.Build(groupId);
        return View(viewModel);
    }
}