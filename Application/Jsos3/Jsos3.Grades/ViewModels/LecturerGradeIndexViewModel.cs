using Jsos3.Grades.Models;

namespace Jsos3.Grades.ViewModels;

public class LecturerGradeIndexViewModel
{
    public required string GroupId { get; set; }
    public required List<StudentDto> Students { get; set; }
}
