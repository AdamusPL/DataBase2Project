using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Faculty
    {
        public string Id {  get; set; }
        public string Name { get; set; }

        public Faculty(string id, string name)
        {
            this.Id = id;
            Name = name;
        }
    }
}
