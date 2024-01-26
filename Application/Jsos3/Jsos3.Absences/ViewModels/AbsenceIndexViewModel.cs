using Jsos3.Absences.Helpers;
using Jsos3.Absences.Models;

namespace Jsos3.Absences.ViewModels;

public class AbsenceIndexViewModel
{
    public required Dictionary<AbsenceKey, StudentAbsenceDto> AbsenceOfStudents { get; set; }
    public required List<StudentInGroupDto> StudentsInGroup { get; set; }
    public required List<DateTime> Days { get; set; }
    public required string GroupId { get; set; }
}
