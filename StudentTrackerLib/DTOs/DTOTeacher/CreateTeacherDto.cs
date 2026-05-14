using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOTeacher
{
    public class CreateTeacherDto
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        
        public CreateTeacherDto()
        {
            Name = string.Empty;
            PasswordHash = string.Empty;
        }
    }
}
