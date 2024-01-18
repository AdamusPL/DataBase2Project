using Jsos3.Grades.Models;
using Jsos3.Grades.Repository;
using Jsos3.Grades.Helpers;

namespace Jsos3.Grades.Services;

public interface IStudentGradeService
{
    Task<List<StudentGrupGradeDto>> GetStudentGrades(int userId, string? groupId);
}
internal class StudentGradeService : IStudentGradeService
{
    private readonly IStudentGradeRepository _studentGradeRepository;

    public StudentGradeService(IStudentGradeRepository studentGradeRepository)
    {
        _studentGradeRepository = studentGradeRepository;
    }

    public async Task<List<StudentGrupGradeDto>> GetStudentGrades(int userId, string? groupId)
    {
        var grades = await _studentGradeRepository.GetStudentGrade(userId, groupId);
        var mapper = new GradeDtoMapper();
        var gradeList = new List<StudentGrupGradeDto>();

        foreach (var grade in grades)
        {
            var gradeDto = new StudentGrupGradeDto
            {
                Id = grade.Id,
                Text = grade.Text,
                Grade = grade.Grade,
                IsFinal = mapper.mapType(grade),
                Accepted = grade.Accepted
            };
            gradeList.Add(gradeDto);
        }
        return gradeList;
    }
}
