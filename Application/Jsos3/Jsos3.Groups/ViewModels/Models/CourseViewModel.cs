using Jsos3.Groups.Models;

namespace Jsos3.Groups.ViewModels.Models;

public class CourseViewModel
{
    public bool ShowAtStartup { get; set; }
    public required StudentCourseDto Course { get; set; }
}