using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOMark
{
    public class CreateMarkDto
    {
        public string Content { get; set; }
        public int StudentId { get; set; }
        public int HeaderId { get; set; }

        public CreateMarkDto()
        {
            Content = string.Empty;
            StudentId = 0;
            HeaderId = 0;
        }

    }
}
