using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Jsos3.Absences.Infrastructure;
using Jsos3.Absences.Infrastructure.Repository;
using Jsos3.Absences.ViewModels;
using Jsos3.Absences.Infrastructure.Models;
using System;
using Jsos3.Absences.Services;
using Jsos3.Absences.Helpers;

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
        if(groupId == null)
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
    public ActionResult Get([FromBody] AbsencePageDto absencePageDto)
    {
        _groupService.AddPresence(absencePageDto);

        return Json(new { success = true });
    }

}
