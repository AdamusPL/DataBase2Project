using Jsos3.Grades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Grades.VievModels
{
    public class LecturerGradeIndexViewModel
    {
        public required string GroupId { get; set; }
        public required List<StudentGradeDto> Students { get; set; }
    }
}
