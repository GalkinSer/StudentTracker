using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group()
        {
            Id = 0;
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Group g)
            {
                return Id.Equals(g.Id) && Name.Equals(g.Name);
            }
            return false;
        }
    }
}
