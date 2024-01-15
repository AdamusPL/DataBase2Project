using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Helpers
{
    public class AbsencePageDto
    {
        public string GroupId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsChecked { get; set; }
    }
}
