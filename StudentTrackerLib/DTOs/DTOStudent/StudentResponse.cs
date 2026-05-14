using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOStudent
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentCardNumber { get; set; }
        public bool IsRepresentative { get; set; }
        public Group Group { get; set; }

        public StudentResponse() 
        {
            Id = 0;
            Name = string.Empty;
            StudentCardNumber = 0;
            IsRepresentative = false;
            Group = new Group();
        }

    }
}
