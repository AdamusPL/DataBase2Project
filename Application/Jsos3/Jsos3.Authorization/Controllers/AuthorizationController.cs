using Jsos3.Authorization.Infrastructure.Repository;
using Jsos3.Authorization.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using IAuthorizationService = Jsos3.Authorization.Services.IAuthorizationService;
using System.Linq;
using Newtonsoft.Json;
using Jsos3.Shared.Auth;

namespace Jsos3.Authorization.Controllers;

public class AuthorizationController(IAuthorizationService _authorizationService, IFieldsOfStudiesRepository _fieldsOfStudiesRepository, IUserAccessor _userAccessor) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!_authorizationService.UserExists(loginViewModel.Login))
        {
            ModelState.AddModelError("", "User does not exist");
            return View("Login", loginViewModel);
        }

        var user = await _authorizationService.AuthenticateUser(loginViewModel.Login, loginViewModel.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid password");
            return View("Login", loginViewModel);
        }

        var tokenString = _authorizationService.GenerateJSONWebToken(user);

        Response.Cookies.Append("AuthToken", tokenString, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        return RedirectToAction("Index", "Home");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Index()
    {
        return View("Login");
    }

    public async Task<IActionResult> Register()
    {
        var options = (await _fieldsOfStudiesRepository.GetFieldsOfStudies()).Select(f => new SelectListItem() { Text = f.Name, Value = f.Id.ToString() }).ToList();
        var model = new RegisterViewModel
        {
            FieldsOfStudiesOptions = options
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var fieldsOfStudiesJson = JsonConvert.SerializeObject(registerViewModel.FieldsOfStudies);

        if (!ModelState.IsValid)
        {
            var options =
            registerViewModel.FieldsOfStudiesOptions = (await _fieldsOfStudiesRepository.GetFieldsOfStudies()).Select(f => new SelectListItem() { Text = f.Name, Value = f.Id.ToString() }).ToList();
            View(registerViewModel);
        }

        var success = await _authorizationService.Register(registerViewModel);

        if(!success)
        {
            ModelState.AddModelError("", "Registration unsuccessful.");
            return View(registerViewModel);
        }

        return RedirectToAction("Login");
    }

    public IActionResult ChangePassword()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        var user = await _authorizationService.AuthenticateUser(_userAccessor.Login, changePasswordViewModel.OldPassword);
        if (user == null)
        {
            ModelState.AddModelError("", "Nieprawidłowe hasło.");
            return View(changePasswordViewModel);
        }

        if(!await _authorizationService.ChangePassword(_userAccessor.Login, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword))
        {
            ModelState.AddModelError("", "Wystąpił nieoczekiwany błąd.");
            return View(changePasswordViewModel);
        }

        return View();
    }
}
