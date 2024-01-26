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
    private readonly IStudentRepository _studentRepository;

    public StudentGradeService(IGradeRepository gradeRepository, IStudentRepository studentRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentGradeDto>> GetStudentGrades(int userId, string groupId)
    {
        var studentId  = await _studentRepository.GetUserStudentId(userId);
        var grades = await _gradeRepository.GetStudentsGrades(groupId, [studentId]);
        return grades
            .SelectMany(x => x.Value)
            .Select(grade => grade.ToDto())
            .ToList();
    }
}
