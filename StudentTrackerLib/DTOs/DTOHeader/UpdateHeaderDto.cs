using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOHeader
{
    public class UpdateHeaderDto
    {
        public string Title { get; set; }
        public UpdateHeaderDto()
        {
            Title = string.Empty;
        }

    }
}
