using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOTeaches
{
    public class CreateTeachesDto
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public CreateTeachesDto()
        {
            SubjectId = 0;
            TeacherId = 0;
        }
    }
}
