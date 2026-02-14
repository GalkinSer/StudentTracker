using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Subject()
        {
            Id = 0;
            Name = string.Empty;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Subject subject)
                return Id.Equals(subject.Id) && Name.Equals(subject.Name);
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
