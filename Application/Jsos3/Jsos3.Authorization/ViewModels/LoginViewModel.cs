using System.ComponentModel.DataAnnotations;

namespace Jsos3.Authorization.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nazwa używtkonika")]
        public string Login { get; set; }
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
