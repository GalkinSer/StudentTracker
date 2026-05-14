using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOSubject
{
    public class CreateSubjectDto
    {
        public string Name { get; set; }

        public CreateSubjectDto()
        {
            Name = string.Empty;
        }
    }
}
