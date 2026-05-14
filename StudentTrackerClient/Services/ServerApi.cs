using StudentTrackerLib.DTOs.DTOAuth;
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

namespace StudentTrackerClient.Services
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

                throw new Exception("Не удаётся подключиться к серверу");
            }
            _serverUri = new Uri(configUri.ToString());
        }

        internal async Task<Teacher> AuthenticateTeacher(Teacher teacher, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;

            var request = new AuthRequestDto()
            {
                Name = teacher.Name,
                PasswordHash = teacher.PasswordHash,
            };
            var content = new StringContent(JsonSerializer.Serialize
                (request, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles }),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/auth", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var data = await response.Content.ReadFromJsonAsync<Teacher>(cancellationToken);

            return data;

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
        internal async Task<IEnumerable<Header>?> GetHeaders(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/headers", cancellationToken);
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
        internal async Task<IEnumerable<Mark>?> GetMarks(CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.BaseAddress = _serverUri;
            try
            {
                var response = await client.GetAsync("/api/marks", cancellationToken);
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
