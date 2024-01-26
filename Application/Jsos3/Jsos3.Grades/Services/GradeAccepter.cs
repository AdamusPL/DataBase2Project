using Jsos3.Grades.Infrastructure.Repository;

namespace Jsos3.Grades.Services;

public interface IGradeAccepter
{
    Task Accept(int gradeId);
    Task Decline(int gradeId);
}

internal class GradeAccepter : IGradeAccepter
{
    private readonly IGradeRepository _gradeRepository;

    public GradeAccepter(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task Accept(int gradeId) =>
        await _gradeRepository.AcceptGrade(gradeId);

    public async Task Decline(int gradeId) =>
        await _gradeRepository.DeclineGrade(gradeId);
}
