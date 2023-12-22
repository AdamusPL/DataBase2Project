using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GroupInfo
    {
        public int DayOfTheWeek { get; set; } // Assuming this represents days as integers (e.g., 1 for Monday, etc.)
        public TimeSpan StartTime { get; set; } // For time values
        public TimeSpan EndTime { get; set; } // For time values
        public string GroupId { get; set; } // Assuming this is an alphanumeric identifier
        public string Classroom { get; set; } // Textual description
        public string Regularity { get; set; } // Textual description, e.g., "Every week"
        public string GroupType { get; set; } // Assuming this is an alphanumeric identifier
        public string LecturerSurname { get; set; } // Textual name
        public string LecturerName { get; set; } // Textual name
        public int LecturerId { get; set; } // Assuming this is a numeric identifier
        public string CourseName { get; set; } // Textual description
        public int ECTS { get; set; } // Assuming ECTS points are represented as integers
    }
}
