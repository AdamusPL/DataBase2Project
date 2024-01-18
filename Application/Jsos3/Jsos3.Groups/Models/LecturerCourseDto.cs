namespace Jsos3.Groups.Models;

public class LecturerCourseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required List<GroupDto> Groups { get; set; }
}
