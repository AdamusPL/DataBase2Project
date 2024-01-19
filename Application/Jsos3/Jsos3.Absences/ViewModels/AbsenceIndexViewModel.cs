using Jsos3.Absences.Helpers;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Absences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.ViewModels
{
    public class AbsenceIndexViewModel
    {
        public required Dictionary<AbsenceKey, StudentAbsenceDto> AbsenceOfStudents { get; set; }
        public required List<StudentInGroupDto> StudentsInGroup { get; set; }
        public required List<DateTime> Days { get; set; }
        public required string GroupId { get; set; }
    }
}
