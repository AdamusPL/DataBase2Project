using Jsos3.Grades.Models;
namespace Jsos3.Grades.Models;

public class StudentGradeDto
{
    public required Student Student { get; set; }
    public required List<StudentGrupGradeDto> Grade { get; set; }
}
