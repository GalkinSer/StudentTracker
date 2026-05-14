using StudentTrackerLib.DTOs.DTOGroup;
using StudentTrackerLib.DTOs.DTOHeader;
using StudentTrackerLib.DTOs.DTOMark;
using StudentTrackerLib.DTOs.DTOStudent;
using StudentTrackerLib.DTOs.DTOSubject;
using StudentTrackerLib.DTOs.DTOTeacher;
using StudentTrackerLib.Models;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace StudentTrackerAdminClient.Services
{
    internal class ServerApi
    {
        private readonly Uri _serverUri;

        internal ServerApi()
        {
            var configUri = ConfigurationManager.AppSettings["serverUri"];
            if (configUri == null)
            {
                MessageBox.Show("Не удалось получить адрес сервера из app.config", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                throw new Exception("Не удаётся подключится к серверу");
            }
            _serverUri = new Uri(configUri.ToString());
        }

        internal async Task<Teacher> AddTeacher(Teacher teacher, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new CreateTeacherDto()
            {
                Name = teacher.Name,
                PasswordHash = teacher.PasswordHash,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/teachers", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadFromJsonAsync<Teacher>(cancellationToken);

            return data;
        }
        internal async Task ChangeTeacher(int teacherToChangeId, Teacher teacher, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateTeacherDto()
            {
                Name = teacher.Name,
                PasswordHash = teacher.PasswordHash,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/teachers/{teacherToChangeId}", content, cancellationToken);
        }
        internal async Task RemoveTeacher(int teacherToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/teachers/{teacherToRemoveId}", cancellationToken);
            return;
        }
        internal async void AssignSubjectForTeacher(int teacherId, int subjectId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.PutAsync($"/api/teachers/{teacherId}-{subjectId}", null, cancellationToken);
            
            return;
        }
        internal async void DeassignSubjectForTeacher(int teacherId, int subjectId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/teachers/{teacherId}-{subjectId}", cancellationToken);

            return;
        }

        internal async Task<Subject> AddSubject(Subject subject, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new CreateSubjectDto()
            {
                Name = subject.Name,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/subjects", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadFromJsonAsync<Subject>(cancellationToken);

            return data;
        }
        internal async Task ChangeSubject(int subjectToChangeId, Subject subject, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateSubjectDto()
            {
                Name = subject.Name
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/subjects/{subjectToChangeId}", content, cancellationToken);
        }
        internal async Task RemoveSubject(int subjectToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/subjects/{subjectToRemoveId}", cancellationToken);
            return;
        }

        internal async Task<Student> AddStudent(Student student, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new CreateStudentDto()
            {
                Name= student.Name,
                IsRepresentative= student.IsRepresentative,
                StudentCardNumber= student.StudentCardNumber,
                GroupId = student.Group.Id,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/students", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadFromJsonAsync<Student>(cancellationToken);

            return data;
        }
        internal async Task ChangeStudent(int studentToChangeId, Student student, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateStudentDto()
            {
                Name = student.Name,
                IsRepresentative = student.IsRepresentative,
                GroupId = student.Group.Id,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/students/{studentToChangeId}", content, cancellationToken);
        }
        internal async Task RemoveStudent(int studentToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/students/{studentToRemoveId}", cancellationToken);
            return;
        }

        internal async Task<Group> AddGroup(Group group, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new CreateGroupDto()
            {
                Name= group.Name,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/groups", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadFromJsonAsync<Group>(cancellationToken);

            return data;
        }
        internal async Task ChangeGroup(int groupToChangeId, Group group, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateGroupDto()
            {
                Name = group.Name,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/groups/{groupToChangeId}", content, cancellationToken);
        }
        internal async Task RemoveGroup(int groupToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/groups/{groupToRemoveId}", cancellationToken);
            return;
        }
        internal async void AssignSubjectForGroup(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.PutAsync($"/api/groups/{groupId}-{subjectId}", null, cancellationToken);

            return;
        }
        internal async void DeassignSubjectForGroup(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/groups/{groupId}-{subjectId}", cancellationToken);

            return;
        }


        internal async Task AddHeader(Header header, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new CreateHeaderDto()
            {
                Title = header.Title,
                GroupId = header.Group.Id,
                SubjectId = header.Subject.Id,
                TeacherId = header.Teacher.Id,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/headers", content, cancellationToken);
            return;
        }
        internal async void ChangeHeader(int headerToChangeId, Header header, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateHeaderDto()
            {
                Title = header.Title,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/headers/{headerToChangeId}", content, cancellationToken);
        }
        internal async void RemoveHeader(int headerToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var response = await client.DeleteAsync($"/api/headers/{headerToRemoveId}", cancellationToken);
            return;
        }

        internal async Task AddMark(Mark mark, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new CreateMarkDto()
            {
                Content = mark.Content,
                HeaderId = mark.Header.Id,
                StudentId = mark.Student.Id,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/marks", content, cancellationToken);
            return;
        }
        internal async void ChangeMark(int markToChangeId, Mark mark, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            var request = new UpdateMarkDto()
            {
                Content = mark.Content
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"/api/marks/{markToChangeId}", content, cancellationToken);
        }
        internal async void RemoveMark(int markToRemoveId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            
            var response = await client.DeleteAsync($"/api/marks/{markToRemoveId}", cancellationToken);
            
            return;
        }

        internal async Task<IEnumerable<Teacher>?> GetTeachers(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/teachers", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Teacher>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Subject>?> GetSubjects(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/subjects", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Subject>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Group>?> GetGroups(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/groups", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Group>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Student>?> GetStudents(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/students", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }

        internal async Task<IEnumerable<Subject>?> GetSubjects(int teacherId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync($"/api/subjects/teacherId:{teacherId}", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Subject>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Group>?> GetGroups(int subjectId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync($"/api/groups/subjectId:{subjectId}", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Group>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Student>?> GetStudents(int groupId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync($"/api/students/groupId:{groupId}", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Header>?> GetHeaders(int teacherId, int subjectId, int groupId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync($"/api/headers/ids:{teacherId}-{subjectId}-{groupId}", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Header>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }
        internal async Task<IEnumerable<Mark>?> GetMarks(int headerId, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync($"/api/marks/headerId:{headerId}", cancellationToken);
                if (response == null || response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<Mark>>(cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }
        }

    }
}
