using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Studies
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }

        public Group Group { get; set; }
        public Subject Subject { get; set; }

        public Studies() 
        {
            SubjectId = 0;
            GroupId = 0;
            Group = new Group();
            Subject = new Subject();
        }
    }
}
