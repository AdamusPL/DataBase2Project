using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Helpers;

internal static class CourseDtoMapper
{
    internal static IEnumerable<CourseDto> ToCourseDto(this IEnumerable<Group> course) => course
        .GroupBy(x => new CourseData(
                x.CourseId,
                x.Course,
                x.Ects,
                $"{x.CourseLecturerName} {x.CourseLecturerSurname}"))
        .Select(x => new CourseDto
        {
            Id = x.Key.Id,
            Name = x.Key.Name,
            Lecturer = x.Key.Lecturer,
            Ects = x.Key.Ects,
            Groups = x.ToGroupDto().ToList()
        });
}
