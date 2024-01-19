using Jsos3.Grades.Builders;
using Jsos3.Grades.Helpers;
using Jsos3.Grades.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Grades.Controllers;

public class LecturerGradeController : Controller
{
    private readonly ILecturerGradeIndexViewModelBuilder _lecturerGradeIndexViewModelBuilder;
    private readonly IGradeAdder _gradeAdder;

    public LecturerGradeController(ILecturerGradeIndexViewModelBuilder lecturerGradeIndexViewModelBuilder, IGradeAdder gradeAdder)
    {
        _lecturerGradeIndexViewModelBuilder = lecturerGradeIndexViewModelBuilder;
        _gradeAdder = gradeAdder;
    }

    public async Task<IActionResult> Index([FromQuery] string groupId)
    {
        var viewModel = await _lecturerGradeIndexViewModelBuilder.Build(groupId);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddGrade([FromBody] NewGradeDto newGrade)
    {
        await _gradeAdder.AddGrade(newGrade);
        return Json(new { success = true });
    }
}