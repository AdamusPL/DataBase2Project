namespace Jsos3.Grades.Models;

public class StudentDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required List<StudentGradeDto> Grade { get; set; }
}
