using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOStudent
{
    public class CreateStudentDto
    {
        public string Name { get; set; }
        public int StudentCardNumber { get; set; }
        public bool IsRepresentative { get; set; }
        public int GroupId { get; set; }

        public CreateStudentDto()
        {
            Name = string.Empty;
            StudentCardNumber = 0;
            IsRepresentative = false;
            GroupId = 0;
        }

    }
}
