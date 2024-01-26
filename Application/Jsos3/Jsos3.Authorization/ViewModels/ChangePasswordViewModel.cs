using System.ComponentModel.DataAnnotations;

namespace Jsos3.Authorization.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Stare hasło")]
        [Required]
        public string OldPassword { get; set; } = "";
        [Display(Name = "Nowe hasło")]
        [Required]
        public string NewPassword { get; set; } = "";
        [Display(Name = "Powtórz nowe hasło")]
        [Required]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
        public string NewPasswordRepeat { get; set; } = "";
    }
}
