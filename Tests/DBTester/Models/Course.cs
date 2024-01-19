using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ECTS { get; set; }

        public Course() { } 
        public Course(string name, int ects)
        {
            Name = name;
            ECTS = ects;
        }
    }
}
