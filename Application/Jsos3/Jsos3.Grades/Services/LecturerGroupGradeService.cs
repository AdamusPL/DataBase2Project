using Jsos3.Grades.Helpers;
using Jsos3.Grades.Infrastructure.Repository;
using Jsos3.Grades.Models;

namespace Jsos3.Grades.Services;

public interface ILecturerGroupGradeService
{
    Task<List<StudentDto>> GetStudentGrades(string groupId);
}
internal class LecturerGroupGradeService : ILecturerGroupGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IStudentRepository _studentRepository;

    public LecturerGroupGradeService(IGradeRepository gradeRepository, IStudentRepository studentRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentDto>> GetStudentGrades(string groupId)
    {
        var students = await _studentRepository.GetStudents(groupId);
        var grades = await _gradeRepository.GetStudentsGrades(groupId, students.Select(x => x.Id));

        return students
            .Select(student => new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Grade = grades.TryGetValue(student.Id, out var studentGrades) ? studentGrades.Select(x => x.ToDto()).ToList() : new List<StudentGradeDto>()
            })
            .ToList();
    }
}
