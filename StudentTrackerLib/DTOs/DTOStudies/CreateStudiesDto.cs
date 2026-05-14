using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOStudies
{
    public class CreateStudiesDto
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }

        public CreateStudiesDto()
        {
            GroupId = 0;
            SubjectId = 0;
        }
    }
}
