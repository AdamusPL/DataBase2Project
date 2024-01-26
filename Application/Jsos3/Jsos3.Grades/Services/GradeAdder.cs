using Jsos3.Grades.Helpers;
using Jsos3.Grades.Infrastructure.Repository;

namespace Jsos3.Grades.Services;

public interface IGradeAdder
{
    Task AddGrade(NewGradeDto newGrade);
}

internal class GradeAdder : IGradeAdder
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IStudentRepository _studentRepository;

    public GradeAdder(IGradeRepository gradeRepository, IStudentRepository studentRepository)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
    }

    public async Task AddGrade(NewGradeDto newGrade)
    {
        if (newGrade.Grade % 0.5m != 0 
            || newGrade.Grade < 2
            || newGrade.Grade > 5.5m)
        {
            throw new InvalidDataException();
        }

        var studentInGroupId = await _studentRepository.GetStudentInGroupId(newGrade.GroupId, newGrade.StudentId);

        await _gradeRepository.AddGrade(
            studentInGroupId,
            newGrade.GradeText,
            newGrade.Grade,
            newGrade.IsFinal);
    }
}
