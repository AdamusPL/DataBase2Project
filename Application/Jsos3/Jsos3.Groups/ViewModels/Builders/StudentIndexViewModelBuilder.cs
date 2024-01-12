using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Models;
using Jsos3.Shared.Auth;

namespace Jsos3.Groups.ViewModels.Builders;

public interface IStudentIndexViewModelBuilder
{
    Task<StudentIndexViewModel> Build(string? semesterId);
}

public class StudentIndexViewModelBuilder : IStudentIndexViewModelBuilder
{
    private readonly IStudentService _studentService;
    private readonly ISemesterService _semesterService;
    private readonly IUserAccessor _userAccessor;

    public StudentIndexViewModelBuilder(IStudentService studentService, IUserAccessor userAccessor, ISemesterService semesterService)
    {
        _studentService = studentService;
        _userAccessor = userAccessor;
        _semesterService = semesterService;
    }

    public async Task<StudentIndexViewModel> Build(string? semesterId)
    {
        var courses = await _studentService.GetCoursesWithGroups(_userAccessor.Id, semesterId);
        var semesters = await _semesterService.GetSemesters();
        return new StudentIndexViewModel
        {
            SelectedSemester = semesterId ?? semesters.First(),
            Semesters = semesters,
            Courses = courses
        };
    }
}
