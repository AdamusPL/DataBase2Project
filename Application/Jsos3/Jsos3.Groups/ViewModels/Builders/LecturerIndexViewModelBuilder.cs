using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Models;
using Jsos3.Shared.Auth;

namespace Jsos3.Groups.ViewModels.Builders;

public interface ILecturerIndexViewModelBuilder
{
    Task<LecturerIndexViewModel> Build(string? semesterId, string? courseName);
}

internal class LecturerIndexViewModelBuilder : ILecturerIndexViewModelBuilder
{
    private readonly IGroupService _groupService;
    private readonly ISemesterService _semesterService;
    private readonly IUserAccessor _userAccessor;

    public LecturerIndexViewModelBuilder(IGroupService groupService, ISemesterService semesterService, IUserAccessor userAccessor)
    {
        _groupService = groupService;
        _semesterService = semesterService;
        _userAccessor = userAccessor;
    }

    public async Task<LecturerIndexViewModel> Build(string? semesterId, string? courseName)
    {
        var courses = await _groupService.GetLecturerCourses(
            _userAccessor.Id,
            semesterId,
            courseName);

        var semesters = await _semesterService.GetSemesters();

        return new LecturerIndexViewModel
        {
            SelectedSemester = semesterId ?? semesters.First(),
            Semesters = semesters,
            Courses = courses
        };
    }
}
