using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Header
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
        public List<Mark> Marks { get; set; }
        public Header()
        {
            Id = 0;
            Title = string.Empty;
            Marks = new List<Mark>();
            SubjectId = 0;
            TeacherId = 0;
            GroupId = 0;
            Subject = new Subject();
            Teacher = new Teacher();
            Group = new Group();
        }
        public Header(List<Mark> marks) : this()
        {
            Marks.Clear();
            foreach (Mark mark in marks)
            {
                mark.Header = this;
                Marks.Add(mark);
            }
            //Marks = marks;
        }
        public Header(Header header)
        {
            Id = header.Id;
            Title = header.Title;
            Marks = new List<Mark>();
            Marks.Clear();
            foreach (Mark mark in header.Marks)
            {
                var newMark = new Mark() { Id = mark.Id, Content = mark.Content, Student = mark.Student, Header = this};
                Marks.Add(newMark);
            }
            SubjectId = header.SubjectId;
            TeacherId = header.TeacherId;
            GroupId = header.GroupId;
            Subject = header.Subject;
            Teacher = header.Teacher;
            Group = header.Group;
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
                //i3 = (bool)header?.Marks?.Equals(Marks);
                bool i4 = Subject.Equals(header.Subject);
                bool i5 = Teacher.Equals(header.Teacher);
                bool i6 = Group.Equals(header.Group);
                return i1 && i2 && i3 && i4 && i5 && i6;
            }
            return false;
        }
        public override string ToString()
        {
            return Title + "/" + Subject.ToString() + "/" + Group.ToString() + "/" + Teacher.ToString();
        }
    }
}
