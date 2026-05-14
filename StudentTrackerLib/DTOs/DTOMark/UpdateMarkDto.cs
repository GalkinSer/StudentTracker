using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOMark
{
    public class UpdateMarkDto
    {
        public string Content { get; set; }

        public UpdateMarkDto()
        {
            Content = string.Empty;
        }
    }
}
