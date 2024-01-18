using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Grades.Models;
public class StudentGrupGradeDto
{
    public required int Id { get; set; }
    public required string Text { get; set; }
    public required decimal Grade { get; set; }
    public required GradeType IsFinal { get; set; }
    public required bool? Accepted { get; set; }
}

