using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HashUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserType Type { get; set; }
        public string Login { get; set; }

    }

    public enum UserType
    {
        Student,
        Lecturer,
        Administrator
    }
}
