using Jsos3.LecturerInformations.Extensions;
using Jsos3.LecturerInformations.Services;
using Jsos3.LecturerInformations.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.LecturerInformations.Controllers;

public class LecturerInformationsController : Controller
{
    private readonly ILecturerService _lecturerService;

    public LecturerInformationsController(ILecturerService lecturerService)
    {
        _lecturerService = lecturerService;
    }

    public async Task<ActionResult> Index([FromQuery] string? searchTerm, [FromQuery] int? pageNumber)
    {
        var lecturersPages = await _lecturerService.GetLecturersPagesCount(searchTerm);
        pageNumber = pageNumber.Clamp(1, lecturersPages);
        var pageIndex = pageNumber.Value == 0 ? 0 : pageNumber.Value - 1;

        var lecturersData = await _lecturerService.GetLecturersAtPage(searchTerm, pageIndex);

        var lecturerInformationsViewModel = new LecturerInformationIndexViewModel()
        {
            LecturersData = lecturersData,
            PageNumber = pageNumber.Value,
            NumberOfPages = lecturersPages
        };

        return View(lecturerInformationsViewModel);
    }
}
