using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Course
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public Course(string name, int eCTS)
        {
            Name = name;
            ECTS = eCTS;
        }
    }
}
