using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Models;
using Jsos3.Shared.Logic;
using Jsos3.Shared.Models;

namespace Jsos3.Groups.Helpers;

internal interface IStudentGroupDtoMapper
{
    List<GroupDto> Map(List<StudentGroup> studentGroups);
    GroupDto Map(StudentGroup studentGroup);
}

internal class StudentGroupDtoMapper : IStudentGroupDtoMapper
{
    private readonly ITranslationService _translationService;

    public StudentGroupDtoMapper(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    public List<GroupDto> Map(List<StudentGroup> studentGroups) =>
        studentGroups.Select(Map).ToList();

    public GroupDto Map(StudentGroup studentGroup) =>
        new()
        {
            Lecturer = $"{studentGroup.LecturerName} {studentGroup.LecturerSurname}",
            DayOfTheWeek = _translationService.Translate((DayOfWeek)studentGroup.DayOfTheWeek),
            Type = _translationService.Translate((GroupType)studentGroup.Type),
            StartTime = studentGroup.StartTime.ToString("hh\\:mm"),
            EndTime = studentGroup.EndTime.ToString("hh\\:mm")
        };
}
