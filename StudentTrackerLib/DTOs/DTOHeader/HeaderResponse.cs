using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOHeader
{
    public class HeaderResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }

        public HeaderResponse()
        {
            Id = 0;
            Title = string.Empty;
            Subject = null;
            Teacher = null;
            Group = null;
        }

    }
}
