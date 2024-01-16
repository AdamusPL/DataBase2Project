using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Services;

public interface IStudentService
{
    Task<List<StudentCourseDto>> GetCoursesWithGroupsFiltered(int userId, string? semesterId, string? courseName);
}

internal class StudentService : IStudentService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IStudentGroupDtoMapper _studentGroupDtoMapper;

    public StudentService(ISemesterRepository semesterRepository, IStudentGroupRepository studentGroupRepository, IStudentGroupDtoMapper studentGroupDtoMapper)
    {
        _semesterRepository = semesterRepository;
        _studentGroupRepository = studentGroupRepository;
        _studentGroupDtoMapper = studentGroupDtoMapper;
    }

    public Task<List<string>> GetSemesters()
    {
        return _semesterRepository.GetAllSemesterIds();
    }

    public async Task<List<StudentCourseDto>> GetCoursesWithGroupsFiltered(int userId, string? semesterId, string? courseName)
    {
        var semester = semesterId ?? await _semesterRepository.GetLatestSemesterId();
        var groups = await _studentGroupRepository.GetStudentGroupsInSemester(userId, semester);

        return groups
            .Where(x => x.Course.Contains(courseName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
            .GroupBy(x => new CourseData(
                x.CourseId,
                x.Course,
                x.Ects,
                $"{x.CourseLecturerName} {x.CourseLecturerSurname}"))
            .Select(x => new StudentCourseDto
            {
                Id = x.Key.Id,
                Name = x.Key.Name,
                Lecturer = x.Key.Lecturer,
                Ects = x.Key.Ects,
                Groups = _studentGroupDtoMapper.Map(x.ToList())
            })
            .ToList();
    }
}
