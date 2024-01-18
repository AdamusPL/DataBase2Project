using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Models;
using Jsos3.Shared.Models;

namespace Jsos3.Groups.Helpers;

internal static class GroupDtoMapper
{
    internal static IEnumerable<GroupDto> ToGroupDto(this IEnumerable<Group> groups) =>
        groups.Select(ToGroupDto);

    internal static GroupDto ToGroupDto(this Group group) =>
        new()
        {
            Id = group.Id,
            Lecturer = $"{group.LecturerName} {group.LecturerSurname}",
            DayOfTheWeek = (DayOfWeek)group.DayOfTheWeek,
            Type = (GroupType)group.Type,
            StartTime = group.StartTime.ToString("hh\\:mm"),
            EndTime = group.EndTime.ToString("hh\\:mm")
        };
}
