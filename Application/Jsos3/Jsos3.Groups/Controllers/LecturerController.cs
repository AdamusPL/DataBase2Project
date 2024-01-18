using Jsos3.Groups.ViewModels.Builders;
using Jsos3.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Groups.Controllers;

public class LecturerController : Controller
{
    private readonly ILecturerIndexViewModelBuilder _lecturerIndexViewModelBuilder;

    public LecturerController(ILecturerIndexViewModelBuilder lecturerIndexViewModelBuilder)
    {
        _lecturerIndexViewModelBuilder = lecturerIndexViewModelBuilder;
    }

    public async Task<IActionResult> Index([FromQuery] string? semesterId, [FromQuery] string? courseName)
    {
        var viewModel = await _lecturerIndexViewModelBuilder.Build(semesterId, courseName);
        return View(viewModel);
    }
}
