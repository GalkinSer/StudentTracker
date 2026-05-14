using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOTeacher
{
    public class TeacherResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public TeacherResponse() 
        {
            Id = 0;
            Name = string.Empty;
            Subjects = new List<Subject>();
        }
    }
}
