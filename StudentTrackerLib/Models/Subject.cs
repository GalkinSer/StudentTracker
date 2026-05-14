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

        public List<Group> Groups { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Header> Headers { get; set; }

        public Subject()
        {
            Id = 0;
            Name = string.Empty;
            Groups = new List<Group>();
            Teachers = new List<Teacher>();
            Headers = new List<Header>();
        }
        public Subject(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Groups = new List<Group>();
            foreach (Group group in subject.Groups)
            {
                Groups.Add(group);
            }
            Teachers = new List<Teacher>();
            foreach (Teacher teacher in subject.Teachers)
            {
                Teachers.Add(teacher);
            }
            Headers = new List<Header>();
            foreach (Header header in subject.Headers)
            {
                Headers.Add(header);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Subject subject)
                return Id.Equals(subject.Id) && Name.Equals(subject.Name);
                    //&& Groups.Equals(subject.Groups) && Teachers.Equals(subject.Teachers)
                    //&& Headers.Equals(subject.Headers);
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
