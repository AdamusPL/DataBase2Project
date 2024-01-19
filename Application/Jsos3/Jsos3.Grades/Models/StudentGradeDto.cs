using Jsos3.Shared.Models;

namespace Jsos3.Grades.Models;

public class StudentGradeDto
{
    public required int Id { get; set; }
    public required string Text { get; set; }
    public required decimal Grade { get; set; }
    public required GradeType IsFinal { get; set; }
    public required bool? Accepted { get; set; }
}