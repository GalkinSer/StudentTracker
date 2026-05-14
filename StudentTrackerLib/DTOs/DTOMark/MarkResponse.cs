using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOMark
{
    public class MarkResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int StudentId { get; set; }
        public int HeaderId { get; set; }

        public Student Student { get; set; }
        
        public MarkResponse()
        {
            Id = 0;
            Content = string.Empty;
            StudentId = 0;
            HeaderId = 0;
            Student = null;
        }
    }
}
