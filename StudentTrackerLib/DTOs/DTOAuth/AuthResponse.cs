using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.DTOs.DTOAuth
{
    public class AuthResponse
    {
        public Teacher Teacher { get; set; }

        public AuthResponse() 
        {
            Teacher = new Teacher();
        }
    }
}
