using Jsos3.Grades.Helpers;
using Jsos3.Grades.Infrastructure.Repository;
using Jsos3.Grades.Models;

namespace Jsos3.Grades.Services;

public interface IStudentGradeService
{
    Task<List<StudentGradeDto>> GetStudentGrades(int userId, string groupId);
}
internal class StudentGradeService : IStudentGradeService
{
    private readonly IGradeRepository _gradeRepository;

    public StudentGradeService(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<List<StudentGradeDto>> GetStudentGrades(int userId, string groupId)
    {
        var grades = await _gradeRepository.GetStudentsGrades(groupId, [userId]);
        return grades
            .SelectMany(x => x.Value)
            .Select(grade => grade.ToDto())
            .ToList();
    }
}
