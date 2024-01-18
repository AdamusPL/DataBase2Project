
using Jsos3.Grades.Models;
using Jsos3.Grades.Repository;

namespace Jsos3.Grades.Services;

public interface ILecturerGroupGradeService
{
    Task<List<StudentGradeDto>> GetStudentGrades(string groupId);
}
internal class LecturerGroupGradeService : ILecturerGroupGradeService
{
    private readonly ILecturerGradePerository _LecturerGradeRepository;
    private readonly IStudentGradeService _StudentGradeService;

    public LecturerGroupGradeService(ILecturerGradePerository lecturerGradeRepository, IStudentGradeService studentGradeService)
    {
        _LecturerGradeRepository = lecturerGradeRepository;
        _StudentGradeService = studentGradeService;
    }

    public async Task<List<StudentGradeDto>> GetStudentGrades(string groupId)
    {
        var students = await _LecturerGradeRepository.GetStudents(groupId);
        var studentGrades = new List<StudentGradeDto>();
        foreach (var student in students)
        {
            var studentGrade = new StudentGradeDto
            {
                Student = student,
                Grade = await _StudentGradeService.GetStudentGrades(student.Id, groupId)
            };
            studentGrades.Add(studentGrade);
        }
        return studentGrades;
    }
}
