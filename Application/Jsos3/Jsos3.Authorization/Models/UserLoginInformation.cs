using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Authorization.Models
{
    public class UserLoginInformation
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
