using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Services;

public interface IStudentService
{
    Task<List<StudentCourseDto>> GetCoursesWithGroupsFiltered(int studentId, string? semesterId, string? courseName);
    Task<KeyValuePair<string, AverageGradeDto>> GetAverageGrade(int studentId, string? semesterId);
}

internal class StudentService : IStudentService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IStudentGroupDtoMapper _studentGroupDtoMapper;
    private readonly IGradeRepository _gradeRepository;

    private const int GradeRequiredToPass = 3;

    public StudentService(ISemesterRepository semesterRepository, IStudentGroupRepository studentGroupRepository, IStudentGroupDtoMapper studentGroupDtoMapper, IGradeRepository gradeRepository)
    {
        _semesterRepository = semesterRepository;
        _studentGroupRepository = studentGroupRepository;
        _studentGroupDtoMapper = studentGroupDtoMapper;
        _gradeRepository = gradeRepository;
    }

    public async Task<KeyValuePair<string, AverageGradeDto>> GetAverageGrade(int studentId, string? semesterId)
    {
        var semester = await GetSemesterId(semesterId);
        var grade = await _gradeRepository.GetAverageGrade(studentId, semester);
        var ectsInSemester = await _semesterRepository.GetStudentAllEcts(studentId, semester);
        var ectsReceivedInSemester = await _semesterRepository.GetStudentReceivedEcts(studentId, semester, GradeRequiredToPass);

        return new(semester, new()
        {
            Grade = grade,
            Ects = ectsInSemester,
            ReceivedEcts = ectsReceivedInSemester
        });
    }

    public async Task<List<StudentCourseDto>> GetCoursesWithGroupsFiltered(int studentId, string? semesterId, string? courseName)
    {
        var semester = await GetSemesterId(semesterId);
        var groups = await _studentGroupRepository.GetStudentGroupsInSemester(studentId, semester);
        var grades = await _gradeRepository.GetStudentCoursesGrades(studentId, semester);

        return groups
            .Where(x => x.Course.Contains(courseName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
            .GroupBy(x => new CourseData(
                x.CourseId,
                x.Course,
                x.Ects,
                $"{x.CourseLecturerName} {x.CourseLecturerSurname}",
                grades.TryGetValue(x.CourseId, out var grade) ? grade : null))
            .Select(x => new StudentCourseDto
            {
                Id = x.Key.Id,
                Name = x.Key.Name,
                Lecturer = x.Key.Lecturer,
                Ects = x.Key.Ects,
                Groups = _studentGroupDtoMapper.Map(x.ToList()),
                Grade = x.Key.Grade
            })
            .ToList();
    }

    private async Task<string> GetSemesterId(string? semesterId) =>
        semesterId ?? await _semesterRepository.GetLatestSemesterId();
}
