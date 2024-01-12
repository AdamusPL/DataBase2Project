using Jsos3.Absences.Helpers;
using Jsos3.Absences.Infrastructure.Repository;
using Jsos3.Absences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Services
{
    public interface IGroupService
    {
        Task<List<DateTime>> GetDatesOfGroup(string groupId);
        Task<List<StudentInGroupDto>> GetSortedStudentsFromGroup(string groupId);
        Task<Dictionary<AbsenceKey, StudentAbsenceDto>> GetAbsencesOfStudentsInGroup(string groupId);
    }

    public class GroupService : IGroupService
    {
        private readonly IGroupOccurrencesCalculator _groupOccurencesCalculator;
        private readonly IAbsencesOfStudentsRepository _absencesOfStudentsRepository;
        private readonly IStudentsInGroupRepository _studentsInGroupRepository;
        private readonly IGroupDatesRepository _groupDatesRepository;

        public GroupService(IGroupOccurrencesCalculator groupOccurencesCalculator, IAbsencesOfStudentsRepository absencesOfStudentsRepository, IGroupDatesRepository groupDatesRepository,
            IStudentsInGroupRepository studentsInGroupRepository) {
            _groupOccurencesCalculator = groupOccurencesCalculator;
            _absencesOfStudentsRepository = absencesOfStudentsRepository;
            _studentsInGroupRepository = studentsInGroupRepository;
            _groupDatesRepository = groupDatesRepository;
        }

        public async Task<Dictionary<AbsenceKey, StudentAbsenceDto>> GetAbsencesOfStudentsInGroup(string groupId)
        {
            var absencesOfStudents = await _absencesOfStudentsRepository.GetAbsencesOfStudentsInGroup(groupId);

            return absencesOfStudents
                .GroupBy(x => new AbsenceKey(x.StudentId, x.Date))
                .ToDictionary(x => x.Key, x => x.Select(absence => new StudentAbsenceDto { Date = absence.Date }).Single());
        }

        public async Task<List<DateTime>> GetDatesOfGroup(string groupId)
        {
            var groupDate = await _groupDatesRepository.GetDatesOfGroup(groupId);
            return _groupOccurencesCalculator.Calculate(groupDate.Start, groupDate.End, groupDate.DayOfTheWeek, groupDate.Regularity);
        }

        public async Task<List<StudentInGroupDto>> GetSortedStudentsFromGroup(string groupId)
        {
            var studentsInGroup = await _studentsInGroupRepository.GetSortedStudentsFromGroup(groupId);
            return studentsInGroup
            .Select(student => new StudentInGroupDto
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Surname = student.Surname
            })
            .ToList();
        }
    }

}
