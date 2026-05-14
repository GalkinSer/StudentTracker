using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOTeaches
{
    public class TeachesResponse
    {
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public TeachesResponse()
        {
            SubjectId = 0;
            TeacherId = 0;
        }
    }
}
