using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Authorization.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Login { get; set; } = string.Empty;

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Potwierdź hasło")]
        [Required(ErrorMessage = "Pole wymagane")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Surname { get; set; }

        [MinLength(1, ErrorMessage = "Należy wybrać conajmniej jedną opcję.")]
        public List<int> FieldsOfStudies { get; set; } = new List<int>();
        public List<SelectListItem> FieldsOfStudiesOptions { get; set; } = new List<SelectListItem>();
    }
}
