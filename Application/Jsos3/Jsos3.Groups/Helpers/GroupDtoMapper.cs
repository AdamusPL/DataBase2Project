using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Models;
using Jsos3.Shared.Models;

namespace Jsos3.Groups.Helpers;

internal static class GroupDtoMapper
{
    internal static IEnumerable<GroupDto> ToGroupDto(this IEnumerable<Group> studentGroups) =>
        studentGroups.Select(ToGroupDto);

    internal static GroupDto ToGroupDto(this Group studentGroup) =>
        new()
        {
            Lecturer = $"{studentGroup.LecturerName} {studentGroup.LecturerSurname}",
            DayOfTheWeek = (DayOfWeek)studentGroup.DayOfTheWeek,
            Type = (GroupType)studentGroup.Type,
            StartTime = studentGroup.StartTime.ToString("hh\\:mm"),
            EndTime = studentGroup.EndTime.ToString("hh\\:mm")
        };
}
