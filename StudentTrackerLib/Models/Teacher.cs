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

        public List<Subject> Subjects { get; set; }
        public List<Header> Headers { get; set; }

        public Teacher()
        {
            Id = 0;
            Name = string.Empty;
            PasswordHash = string.Empty;
            Subjects = new List<Subject>();
            Headers = new List<Header>();
        }
        public Teacher(Teacher teacher)
        {
            Id = teacher.Id;
            Name = teacher.Name;
            PasswordHash = teacher.PasswordHash;
            Subjects = new List<Subject>();
            foreach (Subject subject in teacher.Subjects)
            {
                Subjects.Add(subject);
            }
            Headers = new List<Header>();
            foreach (Header header in teacher.Headers)
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
            if (obj is Teacher t)
            {
                return Id.Equals(t.Id) && Name.Equals(t.Name); //&& PasswordHash.Equals(t.PasswordHash)
                    //&& Subjects.Equals(t.Subjects) && Headers.Equals(t.Headers);
            }
            return false;
        }
        public bool AuthEquals(Teacher otherTeacher)
        {
            return Name.Equals(otherTeacher.Name) && PasswordHash.Equals(otherTeacher.PasswordHash);
        }
    }
}
