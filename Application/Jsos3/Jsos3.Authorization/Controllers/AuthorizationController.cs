using Jsos3.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.Authorization.Controllers;

public class AuthorizationController : Controller
{
    private readonly IDummyInterface _dummyInterface;

    public AuthorizationController(IDummyInterface dummyInterface)
    {
        _dummyInterface = dummyInterface;
    }

    public IActionResult Index()
    {
        return View();
    }
}
