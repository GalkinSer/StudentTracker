using StudentTrackerLib.DTOs.DTOAuth;
using StudentTrackerLib.DTOs.DTOGroup;
using StudentTrackerLib.DTOs.DTOHeader;
using StudentTrackerLib.DTOs.DTOMark;
using StudentTrackerLib.DTOs.DTOStudent;
using StudentTrackerLib.DTOs.DTOStudies;
using StudentTrackerLib.DTOs.DTOSubject;
using StudentTrackerLib.DTOs.DTOTeacher;
using StudentTrackerLib.DTOs.DTOTeaches;
using StudentTrackerLib.Models;

namespace StudentTrackerLib.DTOs
{
    public static class DtoMapper
    {
        public static GroupResponse ToDto(this Group group)
        {
            return new GroupResponse()
            {
                Id = group.Id,
                Name = group.Name,
                Subjects = group.Subjects,
                Students = group.Students,
            };
        }
        public static HeaderResponse ToDto(this Header header)
        {
            return new HeaderResponse()
            {
                Id = header.Id,
                Title = header.Title,
                Subject = header.Subject,
                Group = header.Group,
                Teacher = header.Teacher,
            };
        }
        public static MarkResponse ToDto(this Mark mark)
        {
            return new MarkResponse()
            {
                Id = mark.Id,
                HeaderId = mark.HeaderId,
                Content = mark.Content,
                StudentId = mark.StudentId,
                Student = mark.Student,
            };
        }
        public static StudentResponse ToDto(this Student student)
        {
            return new StudentResponse()
            {
                Id = student.Id,
                Name = student.Name,
                StudentCardNumber = student.StudentCardNumber,
                IsRepresentative = student.IsRepresentative,
                Group = student.Group,
            };
        }
        public static SubjectResponse ToDto(this Subject subject)
        {
            return new SubjectResponse()
            {
                Id = subject.Id,
                Name = subject.Name,
                Teachers = subject.Teachers,
            };
        }
        public static TeacherResponse ToDto(this Teacher teacher)
        {
            return new TeacherResponse()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Subjects = teacher.Subjects,
            };
        }
        public static TeachesResponse ToDto(this Teaches teaches)
        {
            return new TeachesResponse()
            {
                SubjectId = teaches.SubjectId,
                TeacherId = teaches.TeacherId,
            };
        }
        public static StudiesResponse ToDto(this Studies studies)
        {
            return new StudiesResponse()
            {
                SubjectId = studies.SubjectId,
                GroupId = studies.GroupId,
            };
        }
        public static AuthResponse ToAuthDto(this Teacher teacher)
        {
            return new AuthResponse()
            {
                Teacher = teacher
            };
        }
    }
}
