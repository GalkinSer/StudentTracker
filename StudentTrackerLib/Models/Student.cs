using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Student : IComparable<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentCardNumber { get; set; }
        public bool IsRepresentative { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<Mark> Marks { get; set; }

        public Student()
        {
            Id = 0;
            Name = string.Empty;
            StudentCardNumber = 0;
            IsRepresentative = false;
            GroupId = 0;
            Group = new Group();
            Marks = new List<Mark>();
        }
        public Student(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            StudentCardNumber = student.StudentCardNumber;
            IsRepresentative = student.IsRepresentative;
            GroupId = student.GroupId;
            Group = new Group(student.Group);
            Marks = new List<Mark>();
            foreach (var mark in student.Marks)
            {
                Marks.Add(mark);
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is Student student)
            {
                return Id.Equals(student.Id) && Name.Equals(student.Name)
                    && StudentCardNumber.Equals(student.StudentCardNumber) &&
                    IsRepresentative.Equals(student.IsRepresentative) && Group.Equals(student.Group)
                    ;//&& Marks.Equals(student.Marks);
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            return Name.CompareTo(other.Name);
        }
    }
}
