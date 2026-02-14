using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib.Models.Operational
{
    public class Mark
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Header Header { get; set; }
        public string Content { get; set; }

        public Mark()
        {
            Id = 0;
            Student = new Student();
            Content = string.Empty;
            Header = new Header();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Mark mark)
                return Id.Equals(mark.Id) && Student.Equals(mark.Student) 
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
