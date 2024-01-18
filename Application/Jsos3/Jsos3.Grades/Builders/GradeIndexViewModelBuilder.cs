using Jsos3.Shared.Auth;
using Jsos3.Grades.VievModels;
using Jsos3.Grades.Services;

namespace Jsos3.Grades.Builders;

public interface IGradeIndexViewModelBuilder
{
    Task<GradeIndexViewModel> Build(string groupId);
}

public class GradeIndexViewModelBuilder : IGradeIndexViewModelBuilder
{
    private readonly IStudentGradeService _studentGradeService;
    private readonly IUserAccessor _userAccessor;

    public GradeIndexViewModelBuilder(IStudentGradeService studentGradeService, IUserAccessor userAccessor)
    {
        _studentGradeService = studentGradeService;
        _userAccessor = userAccessor;
    }

    public async Task<GradeIndexViewModel> Build(string groupId)
    {
        var grades = await _studentGradeService.GetStudentGrades(_userAccessor.Id, groupId);
        return new GradeIndexViewModel
        {
            Grade = grades
        };
    }
}
