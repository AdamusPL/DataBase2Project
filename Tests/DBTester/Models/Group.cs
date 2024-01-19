using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Group
    {
        public string Id { get; set; }
        public int DayOfTheWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Classroom { get; set; }
        public int Capacity { get; set; }
        public int RegularityId { get; set; }
        public int TypeId { get; set; }
        public int CourseId { get; set; }
        public string SemesterId { get; set; }
    }
}
