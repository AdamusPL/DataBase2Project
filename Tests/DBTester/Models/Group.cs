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
        public string DayOfWeek { get; set; }
        public System.TimeOnly StartTime { get; set; }
        public System.TimeOnly EndTime { get; set;}
        public string Classroom { get; set; }
        public int Capacity { get; set; }

        public Group(string id, string dayOfWeek, TimeOnly startTime, TimeOnly endTime, string classroom, int capacity)
        {
            Id = id;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            Classroom = classroom;
            Capacity = capacity;
        }
    }
}
