using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Jsos3.Absences.Infrastructure;
using Jsos3.Absences.Infrastructure.Repository;
using Jsos3.Absences.ViewModels;
using Jsos3.Absences.Infrastructure.Models;
using System;

namespace Jsos3.Absences.Controllers
{

    public class AbsencesController : Controller
    { 

        public AbsencesController(IAbsencesOfStudentsRepository absencesOfStudentsRepository, IGroupDatesRepository groupDatesRepository)
        {
            _absencesOfStudentsRepository = absencesOfStudentsRepository;
            _groupDatesRepository = groupDatesRepository;
        }

        public readonly IAbsencesOfStudentsRepository _absencesOfStudentsRepository;
        public readonly IGroupDatesRepository _groupDatesRepository;

        // GET: AbsencesController
        public async Task<ActionResult> Index([FromQuery]string groupId)
        {
            GroupDate GroupDate = await _groupDatesRepository.GetDatesOfGroup(groupId);
            GroupOccurencesCalculator groupOccurencesCalculator = new GroupOccurencesCalculator();


            var absenceIndexViewModel = new AbsenceIndexViewModel()
            {
                AbsenceOfStudents = await _absencesOfStudentsRepository.GetAbsencesOfStudentsInGroup(groupId),
                Days = groupOccurencesCalculator.Calculate(GroupDate.Start, GroupDate.End, GroupDate.DayOfTheWeek, GroupDate.RegularityId)
            };

            return View(absenceIndexViewModel);
        }

    }
}
