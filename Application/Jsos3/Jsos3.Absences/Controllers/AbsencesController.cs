using Jsos3.Absences.Helpers;
using Jsos3.Absences.Services;
using Jsos3.Absences.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Absences.Controllers;

public class AbsencesController : Controller
{
    private readonly IGroupService _groupService;

    public AbsencesController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public async Task<ActionResult> Index([FromQuery] string groupId)
    {
        if (groupId == null)
        {
            return RedirectToAction("Error");
        }

        var absenceIndexViewModel = new AbsenceIndexViewModel()
        {
            GroupId = groupId,
            AbsenceOfStudents = await _groupService.GetAbsencesOfStudentsInGroup(groupId),
            Days = await _groupService.GetDatesOfGroup(groupId),
            StudentsInGroup = await _groupService.GetSortedStudentsFromGroup(groupId)
        };

        return View(absenceIndexViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleAbsence([FromBody] AbsencePageDto absencePageDto)
    {
        await _groupService.UpdatePresence(absencePageDto);

        return Json(new { success = true });
    }

}
