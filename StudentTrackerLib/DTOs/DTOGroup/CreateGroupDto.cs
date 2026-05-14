using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOGroup
{
    public class CreateGroupDto
    {
        public string Name { get; set; }

        public CreateGroupDto() 
        {
            Name = string.Empty;
        }
    }
}
