using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOSubject
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }

        public SubjectResponse() 
        {
            Id = 0;
            Name = string.Empty;
            Teachers = new List<Teacher>();
        }
    }
}
