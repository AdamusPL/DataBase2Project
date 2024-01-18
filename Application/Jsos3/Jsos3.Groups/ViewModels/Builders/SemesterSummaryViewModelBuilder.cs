using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public interface ISemesterSummaryViewModelBuilder
{
    Task<SemesterSummaryViewModel> Build(int studentId, string? semesterId);
}

public class SemesterSummaryViewModelBuilder : ISemesterSummaryViewModelBuilder
{
    private readonly IStudentService _studentService;

    public SemesterSummaryViewModelBuilder(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<SemesterSummaryViewModel> Build(int studentId, string? semesterId)
    {
        var averageGradeInfo = await _studentService.GetAverageGrade(studentId, semesterId);

        return new()
        {
            SelectedSemester = averageGradeInfo.Key,
            GradeInfo = averageGradeInfo.Value
        };
    }
}
