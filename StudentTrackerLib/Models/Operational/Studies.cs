using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models.Operational
{
    public class Studies
    {
        public Group Group { get; set; }
        public Subject Subject { get; set; }

        public Studies() 
        {
            Group = new Group();
            Subject = new Subject();
        }
    }
}
