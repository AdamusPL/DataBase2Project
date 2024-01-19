using Jsos3.Grades.Services;
using Jsos3.Grades.ViewModels;

namespace Jsos3.Grades.Builders;

public interface ILecturerGradeIndexViewModelBuilder
{
    Task<LecturerGradeIndexViewModel> Build(string groupId);
}

public class LecturerGradeIndexViewModelBiulder : ILecturerGradeIndexViewModelBuilder
{
    private readonly ILecturerGroupGradeService _service;

    public LecturerGradeIndexViewModelBiulder(ILecturerGroupGradeService service)
    {
        _service = service;
    }

    public async Task<LecturerGradeIndexViewModel> Build(string groupId)
    {
        var model = new LecturerGradeIndexViewModel
        {
            GroupId = groupId,
            Students = await _service.GetStudentGrades(groupId)
        };
        return model;
    }
}
