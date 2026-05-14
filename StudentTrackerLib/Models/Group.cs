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

        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Header> Headers { get; set; }

        public Group()
        {
            Id = 0;
            Name = string.Empty;
            Students = new List<Student>();
            Subjects = new List<Subject>();
            Headers = new List<Header>();
        }
        public Group(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            Students = new List<Student>();
            foreach (var student in group.Students)
            {
                Students.Add(student);
            }
            Subjects = new List<Subject>();
            foreach (var subject in group.Subjects)
            {
                Subjects.Add(subject);
            }    
            Headers = new List<Header>();
            foreach (var header in group.Headers)
            {
                Headers.Add(header);
            }
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
                    //&& Students.Equals(g.Students) && Subjects.Equals(g.Subjects)
                    //&& Headers.Equals(g.Headers);
            }
            return false;
        }
    }
}
