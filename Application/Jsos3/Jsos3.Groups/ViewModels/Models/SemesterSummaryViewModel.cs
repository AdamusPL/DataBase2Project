using Jsos3.Groups.Models;

namespace Jsos3.Groups.ViewModels.Models;

public class SemesterSummaryViewModel
{
    public required string SelectedSemester { get; set; }
    public required AverageGradeDto GradeInfo { get; set; }
}
