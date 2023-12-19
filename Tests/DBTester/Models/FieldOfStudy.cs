using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FieldOfStudy
    {
        public string Name { get; set; }
        public int Degree { get; set; }
        public string FacultyId { get; set; }

        public FieldOfStudy(string name, int degree, string facultyId)
        {
            Name = name;
            Degree = degree;
            FacultyId = facultyId;
        }
    }
}
