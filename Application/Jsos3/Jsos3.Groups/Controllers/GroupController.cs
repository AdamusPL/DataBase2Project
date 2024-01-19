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
        return Redirect(_userAccessor.Type switch
        {
            UserType.Student => "/Student",
            UserType.Lecturer => "/Lecturer",
            _ => "/Error"
        });
    }
}
