using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOStudies
{
    public class StudiesResponse
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }

        public StudiesResponse()
        {
            GroupId = 0;
            SubjectId = 0;
        }
    }
}
