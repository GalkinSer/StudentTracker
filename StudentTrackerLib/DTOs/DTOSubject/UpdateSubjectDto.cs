using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOSubject
{
    public class UpdateSubjectDto
    {
        public string Name { get; set; }

        public UpdateSubjectDto() 
        {
            Name = string.Empty;
        }
    }
}
