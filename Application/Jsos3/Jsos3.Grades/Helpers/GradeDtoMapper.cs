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
        public GradeType mapType(StudentGrade studentGrade)
        {
            if (studentGrade.IsFinal)
            {
                return GradeType.Końcowa;
            }
            else
            {
                return GradeType.Cząstkowa;
            }
        }
    }
}
