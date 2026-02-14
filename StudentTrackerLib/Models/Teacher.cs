using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        public Teacher()
        {
            Id = 0;
            Name = string.Empty;
            PasswordHash = string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Teacher t)
            {
                return Id.Equals(t.Id) && Name.Equals(t.Name) && PasswordHash.Equals(t.PasswordHash);
            }
            return false;
        }
        public bool AuthEquals(Teacher otherTeacher)
        {
            return Name.Equals(otherTeacher.Name) && PasswordHash.Equals(otherTeacher.PasswordHash);
        }
    }
}
