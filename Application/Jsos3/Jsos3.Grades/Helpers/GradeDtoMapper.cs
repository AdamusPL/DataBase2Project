using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsos3.Grades.Models;

namespace Jsos3.Grades.Helpers
{
    public class GradeDtoMapper
    {
        public gradeType mapType(StudentGrade studentGrade)
        {
            if (studentGrade.IsFinal)
            {
                return gradeType.Końcowa;
            }
            else
            {
                return gradeType.Cząstkowa;
            }
        }
    }
}
