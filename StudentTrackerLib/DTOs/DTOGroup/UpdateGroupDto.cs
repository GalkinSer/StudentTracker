using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOGroup
{
    public class UpdateGroupDto
    {
        public string Name { get; set; }
        public UpdateGroupDto() 
        {
            Name = string.Empty;
        }
    }
}
