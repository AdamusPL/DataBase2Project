using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Logic;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Services;

public interface IGroupService
{
    Task<List<CourseDto>> GetLecturerCourses(int lecturerId, string? semesterId, string? courseName);
    Task<List<CourseDto>> GetStudentCourses(int studentId, string? semesterId, string? courseName);
}

internal class GroupService : IGroupService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IGroupFilter _groupFilter;

    public GroupService(ISemesterRepository semesterRepository, IGroupRepository groupRepository, IGroupFilter groupFilter)
    {
        _semesterRepository = semesterRepository;
        _groupRepository = groupRepository;
        _groupFilter = groupFilter;
    }

    public Task<List<CourseDto>> GetLecturerCourses(int lecturerId, string? semesterId, string? courseName) =>
        GetCourses(
            semesterId,
            courseName,
            semester => _groupRepository.GetLecturerGroupsInSemester(lecturerId, semester));

    public Task<List<CourseDto>> GetStudentCourses(int studentId, string? semesterId, string? courseName) =>
        GetCourses(
            semesterId,
            courseName,
            semester => _groupRepository.GetStudentGroupsInSemester(studentId, semester));

    private async Task<List<CourseDto>> GetCourses(
        string? semesterId,
        string? courseName,
        Func<string, Task<List<Group>>> groupsFetcher)
    {
        var semester = semesterId ?? await _semesterRepository.GetLatestSemesterId();
        var groups = await groupsFetcher(semester);

        return _groupFilter
            .Filter(groups, courseName)
            .ToCourseDto()
            .ToList();
    }
}
