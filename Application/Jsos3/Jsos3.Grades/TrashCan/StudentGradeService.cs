namespace Jsos3.Grades.TrashCan;

public interface IStudentGradeService
{
    Task<StudentGrupGradeDto> GetStudentGrades(int userId, string? groupId);
}
internal class StudentGradeService
{
    private readonly StudentGradeRepository _studentGradeRepository;

    public StudentGradeService(StudentGradeRepository studentGradeRepository)
    {
        _studentGradeRepository = studentGradeRepository;
    }

    public async Task<StudentGrupGradeDto> GetStudentGradesAsync(int userId, string? groupId)
    {
        var grades = await _studentGradeRepository.GetStudentGrade(userId, groupId);
        return new StudentGrupGradeDto { Grades = grades };
    }
}
