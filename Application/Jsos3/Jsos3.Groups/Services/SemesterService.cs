using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Models;

namespace Jsos3.Groups.Services;

public interface ISemesterService
{
    Task<List<string>> GetSemesters();
    Task<KeyValuePair<string, AverageGradeDto>> GetStudentAverageGrade(int studentId, string? semesterId);
}

internal class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGradeRepository _gradeRepository;

    private const int GradeRequiredToPass = 3;

    public SemesterService(ISemesterRepository semesterRepository, IGradeRepository gradeRepository)
    {
        _semesterRepository = semesterRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<List<string>> GetSemesters()
    {
        return await _semesterRepository.GetAllSemesterIds();
    }

    public async Task<KeyValuePair<string, AverageGradeDto>> GetStudentAverageGrade(int studentId, string? semesterId)
    {
        var semester = semesterId ?? await _semesterRepository.GetLatestSemesterId(); ;
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
}
