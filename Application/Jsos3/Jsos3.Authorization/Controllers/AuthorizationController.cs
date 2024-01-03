using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Authorization.Controllers;

public class AuthorizationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
