namespace Jsos3.Groups.Models;

public class CourseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Lecturer { get; set; }
    public required int Ects { get; set; }
    public required List<GroupDto> Groups { get; set; }
}
