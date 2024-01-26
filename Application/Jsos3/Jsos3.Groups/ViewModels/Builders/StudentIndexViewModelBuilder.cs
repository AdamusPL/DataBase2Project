using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Models;
using Jsos3.Shared.Auth;

namespace Jsos3.Groups.ViewModels.Builders;

public interface IStudentIndexViewModelBuilder
{
    Task<StudentIndexViewModel> Build(string? semesterId, string? courseName);
}

public class StudentIndexViewModelBuilder : IStudentIndexViewModelBuilder
{
    private readonly IGroupService _groupService;
    private readonly ISemesterService _semesterService;
    private readonly IUserAccessor _userAccessor;
    private readonly ISemesterSummaryViewModelBuilder _semesterSummaryViewModelBuilder;

    public StudentIndexViewModelBuilder(
        IGroupService groupService,
        IUserAccessor userAccessor,
        ISemesterService semesterService,
        ISemesterSummaryViewModelBuilder semesterSummaryViewModelBuilder)
    {
        _groupService = groupService;
        _userAccessor = userAccessor;
        _semesterService = semesterService;
        _semesterSummaryViewModelBuilder = semesterSummaryViewModelBuilder;
    }

    public async Task<StudentIndexViewModel> Build(string? semesterId, string? courseName)
    {
        var courses = await _groupService.GetStudentCourses(
            _userAccessor.Id,
            semesterId,
            courseName);

        var semesters = await _semesterService.GetSemesters();
        var semesterSummary = await _semesterSummaryViewModelBuilder.Build(_userAccessor.Id, semesterId);

        return new StudentIndexViewModel
        {
            SemesterSummary = semesterSummary,
            Semesters = semesters,
            Courses = courses
        };
    }
}
