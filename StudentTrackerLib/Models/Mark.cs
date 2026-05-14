using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int StudentId { get; set; }
        public int HeaderId { get; set; }
        public Student Student { get; set; }
        public Header Header { get; set; }

        public Mark()
        {
            Id = 0;
            Content = string.Empty;

            HeaderId = 0;
            StudentId = 0;
            Student = new Student();
            Header = new Header();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Mark mark)
                return Id.Equals(mark.Id)// && Student.Equals(mark.Student) 
                    //&& Header.Equals(mark.Header)
                    && Content.Equals(mark.Content);
            return false;
        }
        public override string ToString()
        {
            return Student.ToString() + "/" + Content;
        }
    }
}
