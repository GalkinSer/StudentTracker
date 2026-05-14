using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Teaches
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }

        public Teaches() 
        {
            SubjectId = 0;
            TeacherId = 0;

            Subject = new Subject();
            Teacher = new Teacher();
        }
    }
}
