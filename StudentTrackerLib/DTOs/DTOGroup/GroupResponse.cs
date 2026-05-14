using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOGroup
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }
        public GroupResponse() 
        {
            Id = 0;
            Name = string.Empty;
            Subjects = new List<Subject>();
            Students = new List<Student>();
        }

    }
}
