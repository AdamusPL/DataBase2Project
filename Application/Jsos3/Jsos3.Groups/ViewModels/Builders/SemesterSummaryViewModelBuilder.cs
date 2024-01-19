using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public interface ISemesterSummaryViewModelBuilder
{
    Task<SemesterSummaryViewModel> Build(int studentId, string? semesterId);
}

public class SemesterSummaryViewModelBuilder : ISemesterSummaryViewModelBuilder
{
    private readonly ISemesterService _semesterService;

    public SemesterSummaryViewModelBuilder(ISemesterService semesterService)
    {
        _semesterService = semesterService;
    }

    public async Task<SemesterSummaryViewModel> Build(int studentId, string? semesterId)
    {
        var averageGradeInfo = await _semesterService.GetStudentAverageGrade(studentId, semesterId);

        return new()
        {
            SelectedSemester = averageGradeInfo.Key,
            GradeInfo = averageGradeInfo.Value
        };
    }
}
