using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models.Operational
{
    public class Header
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Mark> Marks { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
        public bool IsChanged { get; set; }
        public Header()
        {
            Id = 0;
            Title = string.Empty;
            Marks = new List<Mark>();
            Subject = new Subject();
            Teacher = new Teacher();
            Group = new Group();
            IsChanged = false;
        }
        public Header(List<Mark> marks) : this()
        {
            foreach (Mark mark in marks)
            {
                mark.Header = this;
            }
            Marks = marks;
        }
        public Header(Header header)
        {
            Id = header.Id;
            Title = header.Title;
            Marks = header.Marks;
            Subject = header.Subject;
            Teacher = header.Teacher;
            Group = header.Group;
            IsChanged = header.IsChanged;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Header header)
            {
                bool i1 = Id.Equals(header.Id); 
                bool i2 = Title.Equals(header.Title);
                bool i3 = true;
                if (header.Marks != null && Marks != null)
                {
                    if (header.Marks.Count == Marks.Count)
                    {
                        for (int i = 0; i < Marks.Count; i++)
                        {
                            if (!header.Marks[i].Equals(Marks[i]))
                            {
                                i3 = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        i3 = false;
                    }
                }
                else if (header.Marks == null && Marks == null)
                {
                    i3 = true;
                }
                else i3 = false;
                bool i4 = Subject.Equals(header.Subject);
                bool i5 = Teacher.Equals(header.Teacher);
                bool i6 = Group.Equals(header.Group);
                return i1 && i2 /*&& i3*/ && i4 && i5 && i6;
            }
            return false;
        }
        public override string ToString()
        {
            return Title + "/" + Subject.ToString() + "/" + Group.ToString() + "/" + Teacher.ToString();
        }
    }
}
