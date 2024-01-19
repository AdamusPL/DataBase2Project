using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.Models;

public class LecturerCourseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required List<GroupViewModel> Groups { get; set; }
}
