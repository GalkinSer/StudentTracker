using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models.Operational
{
    public class Teaches
    {
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }

        public Teaches() 
        {
            Subject = new Subject();
            Teacher = new Teacher();
        }
    }
}
