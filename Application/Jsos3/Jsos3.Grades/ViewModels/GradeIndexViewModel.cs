using Jsos3.Grades.Models;

namespace Jsos3.Grades.ViewModels;

public class GradeIndexViewModel
{
    public required List<StudentGradeDto> Grade { get; set; }
    public required string GroupId { get; set; }
}
