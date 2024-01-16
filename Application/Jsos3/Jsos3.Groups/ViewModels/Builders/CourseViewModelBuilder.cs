using Jsos3.Groups.Models;
using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public interface ICourseViewModelBuilder
{
    List<CourseViewModel> BuildList(List<StudentCourseDto> courses);
}

internal class CourseViewModelBuilder : ICourseViewModelBuilder
{
    public List<CourseViewModel> BuildList(List<StudentCourseDto> courses)
    {
        var result = new List<CourseViewModel>();

        foreach (var course in courses.Select((value, i) => new { Index = i, Value = value }))
        {
            result.Add(new CourseViewModel
            {
                Course = course.Value,
                ShowAtStartup = course.Index == 0
            });
        }

        return result;
    }
}
