using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOTeacher
{
    public class UpdateTeacherDto
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        
        public UpdateTeacherDto() 
        {
            Name = string.Empty;
            PasswordHash = string.Empty;
        }
    }
}
