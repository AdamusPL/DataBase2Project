using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Models;
using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.Helpers;

internal static class CourseDtoMapper
{
    internal static IEnumerable<StudentCourseDto> ToStudentCourseDto(this IEnumerable<Group> course, Dictionary<int, decimal> grades) => course
        .GroupBy(x => new StudentCourseKey(
                x.CourseId,
                x.Course,
                x.Ects,
                $"{x.CourseLecturerName} {x.CourseLecturerSurname}",
                grades.TryGetValue(x.CourseId, out var grade) ? grade : null))
        .Select(x => new StudentCourseDto
        {
            Id = x.Key.Id,
            Name = x.Key.Name,
            Lecturer = x.Key.Lecturer,
            Ects = x.Key.Ects,
            Grade = grades.ContainsKey(x.Key.Id) ? grades[x.Key.Id] : null,
            Groups = x
                .ToGroupDto()
                .Select(x => new GroupViewModel
                {
                    ShowAbsenceLink = false,
                    Group = x
                })
                .ToList()
        });

    internal static IEnumerable<LecturerCourseDto> ToLecturerCourseDto(this IEnumerable<Group> course) => course
        .GroupBy(x => new LecturerCourseKey(
                x.CourseId,
                x.Course))
        .Select(x => new LecturerCourseDto
        {
            Id = x.Key.Id,
            Name = x.Key.Name,
            Groups = x
                .ToGroupDto()
                .Select(x => new GroupViewModel
                {
                    ShowAbsenceLink = true,
                    Group = x
                })
                .ToList()
        });
}
