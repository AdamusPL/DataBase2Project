using Jsos3.Groups.Models;

namespace Jsos3.Groups.ViewModels.Models;

public class LecturerIndexViewModel
{
    public required string SelectedSemester { get; set; }
    public required List<string> Semesters { get; set; }
    public required List<CourseDto> Courses { get; set; }
}
