using Jsos3.Shared.Auth;
using Jsos3.User.Infrastructure.Repository;
using Jsos3.User.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Jsos3.User.Controllers;

public class UserController(IUserEmailRepository _userEmailRepository, IUserAccessor _userAccessor) : Controller
{
    public IActionResult Index()
    {
        return View("Settings");
    }

    public IActionResult Settings()
    {
        return View("Settings");
    }

    public async Task<IActionResult> EditEmail()
    {
        var model = new EditEmailViewModel()
        {
            CurrentEmails = await _userEmailRepository.GetEmails(_userAccessor.Id)
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmail(EditEmailViewModel model)
    {
        model.CurrentEmails = await _userEmailRepository.GetEmails(_userAccessor.Id);

        if (model.CurrentEmails.Contains(model.NewEmail))
        {
            ModelState.AddModelError("", "Podany email jest już przypisany do konta.");

            return View("EditEmail", model);
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Nieprawidłowy email.");

            return View("EditEmail", model);
        }       

        if (!(await _userEmailRepository.AddEmail(_userAccessor.Id, model.NewEmail)))
        {
            ModelState.AddModelError("", "Wystąpił nieoczekiwany błąd.");

            return View("EditEmail", model);
        }
                
        model.CurrentEmails.Add(model.NewEmail);
        model.NewEmail = "";

        return View("EditEmail", model);
    }

    [HttpPost]
    public JsonResult RemoveEmail(string email)
    {
        var result = _userEmailRepository.RemoveEmail(_userAccessor.Id, email);
        return Json(new { success = result });
    }
}
