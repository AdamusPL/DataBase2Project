using Jsos3.Groups.ViewModels.Builders;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Groups.Controllers;

public class StudentController : Controller
{
    private readonly IStudentIndexViewModelBuilder _studentIndexViewModelBuilder;

    public StudentController(IStudentIndexViewModelBuilder studentIndexViewModelBuilder)
    {
        _studentIndexViewModelBuilder = studentIndexViewModelBuilder;
    }

    public async Task<IActionResult> Index([FromQuery] string? semesterId)
    {
        var viewModel = await _studentIndexViewModelBuilder.Build(semesterId);
        return View(viewModel);
    }
}
