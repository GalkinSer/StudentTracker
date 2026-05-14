using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOStudent
{
    public class UpdateStudentDto
    {
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }
        public int GroupId { get; set; }

        public UpdateStudentDto()
        {
            Name = string.Empty;
            IsRepresentative = false;
            GroupId = 0;
        }

    }
}
