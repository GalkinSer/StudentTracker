using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOAuth
{
    public class AuthRequestDto
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        public AuthRequestDto() 
        {
            Name = string.Empty;
            PasswordHash = string.Empty;
        }
    }
}
