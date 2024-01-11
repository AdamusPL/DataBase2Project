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

        public AbsencesController(IAbsencesOfStudentsRepository absencesOfStudentsRepository, IGroupDatesRepository groupDatesRepository, IStudentsInGroupRepository studentsInGroupRepository)
        {
            _absencesOfStudentsRepository = absencesOfStudentsRepository;
            _groupDatesRepository = groupDatesRepository;
            _studentsInGroupRepository = studentsInGroupRepository;
        }

        public readonly IAbsencesOfStudentsRepository _absencesOfStudentsRepository;
        public readonly IGroupDatesRepository _groupDatesRepository;
        public readonly IStudentsInGroupRepository _studentsInGroupRepository;

        // GET: AbsencesController
        public async Task<ActionResult> Index([FromQuery]string groupId)
        {
            var GroupDate = await _groupDatesRepository.GetDatesOfGroup(groupId);
            var groupOccurencesCalculator = new GroupOccurencesCalculator();


            var AbsenceOfStudentsList = await _absencesOfStudentsRepository.GetAbsencesOfStudentsInGroup(groupId);
            var AbsenceOfStudents = AbsenceOfStudentsList
                .GroupBy(x => new AbsenceKey(x.StudentId, x.Date))
                .ToDictionary(x => x.Key, x => x.Single());

            var absenceIndexViewModel = new AbsenceIndexViewModel()
            {
                GroupId = groupId,
                AbsenceOfStudents = AbsenceOfStudents,
                Days = groupOccurencesCalculator.Calculate(GroupDate.Start, GroupDate.End, GroupDate.DayOfTheWeek, GroupDate.RegularityId),
                StudentsInGroup = await _studentsInGroupRepository.GetStudentsFromGroup(groupId)
            };

            return View(absenceIndexViewModel);
        }

    }
}
