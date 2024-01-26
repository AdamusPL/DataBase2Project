using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.User.ViewModels
{
    public class EditEmailViewModel
    {
        public List<string> CurrentEmails { get; set; } = new List<string>();

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Nowy mail")]
        public string NewEmail { get; set; }
    }
}
