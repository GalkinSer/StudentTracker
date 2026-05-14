using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOHeader
{
    public class CreateHeaderDto
    {
        public string Title { get; set; }

        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public CreateHeaderDto()
        {
            Title = string.Empty;
            SubjectId = 0;
            TeacherId = 0;
            GroupId = 0;
        }
    }
}
