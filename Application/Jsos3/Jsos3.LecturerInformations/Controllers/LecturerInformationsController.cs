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

        pageNumber ??= 1;
        if (pageNumber.Value > lecturersPages)
        {
            pageNumber = lecturersPages;
        }

        var lecturersData = await _lecturerService.GetLecturersAtPage(searchTerm, pageNumber - 1);

        var lecturerInformationsViewModel = new LecturerInformationIndexViewModel()
        {
            LecturersData = lecturersData,
            PageNumber = pageNumber.Value,
            NumberOfPages = lecturersPages
        };

        return View(lecturerInformationsViewModel);
    }
}
