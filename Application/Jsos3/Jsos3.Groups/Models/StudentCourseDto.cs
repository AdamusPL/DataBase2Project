namespace Jsos3.Groups.Models;

public class StudentCourseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Lecturer { get; set; }
    public required int Ects { get; set; }
    public required decimal? Grade { get; set; }
    public required List<GroupDto> Groups { get; set; }
}
