using Jsos3.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Groups.Controllers;

public class GroupController : Controller
{
    private readonly IUserAccessor _userAccessor;

    public GroupController(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }

    public IActionResult Index()
    {
        return _userAccessor.Type switch
        {
            UserType.Student => RedirectToAction("Index", "Student"),
            UserType.Lecturer => RedirectToAction("Index", "Lecturer"),
            _ => RedirectToAction("Error", "Home")
        };
    }
}
