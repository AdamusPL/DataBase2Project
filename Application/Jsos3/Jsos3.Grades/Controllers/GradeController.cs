using Jsos3.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Grades.Controllers;

public class GradeController : Controller
{
    private readonly IUserAccessor _userAccessor;

    public GradeController(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }

    public IActionResult Index([FromQuery] string groupId)
    {
        return _userAccessor.Type switch
        {
            UserType.Lecturer => RedirectToAction("Index", "LecturerGrade", new { groupId }),
            UserType.Student => RedirectToAction("Index", "StudentGrade", new { groupId }),
            _ => RedirectToAction("Error", "Home")
        };
    }
}
