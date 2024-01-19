using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Logic;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Services;

public interface IGroupService
{
    Task<List<LecturerCourseDto>> GetLecturerCourses(int lecturerId, string? semesterId, string? courseName);
    Task<List<StudentCourseDto>> GetStudentCourses(int studentId, string? semesterId, string? courseName);
}

internal class GroupService : IGroupService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IGroupFilter _groupFilter;
    private readonly IGradeRepository _gradeRepository;

    public GroupService(ISemesterRepository semesterRepository, IGroupRepository groupRepository, IGroupFilter groupFilter, IGradeRepository gradeRepository)
    {
        _semesterRepository = semesterRepository;
        _groupRepository = groupRepository;
        _groupFilter = groupFilter;
        _gradeRepository = gradeRepository;
    }

    public Task<List<LecturerCourseDto>> GetLecturerCourses(int lecturerId, string? semesterId, string? courseName) =>
        GetCourses(
            semesterId,
            courseName,
            semester => _groupRepository.GetLecturerGroupsInSemester(lecturerId, semester),
            groups => groups.ToLecturerCourseDto());

    public async Task<List<StudentCourseDto>> GetStudentCourses(int studentId, string? semesterId, string? courseName)
    {
        var grades = await _gradeRepository.GetStudentCoursesGrades(studentId, await _semesterRepository.GetLatestSemesterId());

        return await GetCourses(
            semesterId,
            courseName,
            semester => _groupRepository.GetStudentGroupsInSemester(studentId, semester),
            groups => groups.ToStudentCourseDto(grades));
    }

    private async Task<List<TDto>> GetCourses<TDto>(
        string? semesterId,
        string? courseName,
        Func<string, Task<List<Group>>> groupsFetcher,
        Func<IEnumerable<Group>, IEnumerable<TDto>> dtoMapper)
    {
        var semester = semesterId ?? await _semesterRepository.GetLatestSemesterId();
        var groups = await groupsFetcher(semester);

        return dtoMapper(_groupFilter.Filter(groups, courseName))
            .ToList();
    }
}
