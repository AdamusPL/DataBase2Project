namespace Jsos3.Grades.TrashCan;

public interface IStudentGradeService
{
    Task<StudentGrupGradeDto> GetStudentGrades(int userId, string? groupId);
}
internal class StudentGradeService : IStudentGradeService
{
    private readonly IStudentGradeRepository _studentGradeRepository;

    public StudentGradeService(IStudentGradeRepository studentGradeRepository)
    {
        _studentGradeRepository = studentGradeRepository;
    }

    public async Task<StudentGrupGradeDto> GetStudentGrades(int userId, string? groupId)
    {
        var grades = await _studentGradeRepository.GetStudentGrade(userId, groupId);
        return new StudentGrupGradeDto { Grades = grades };
    }
}
