using StudentTrackerAdminClient.Services;
using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using StudentTrackerLib.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels.TabPagesViewModels
{
    public class StudentsTabPageViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private Student _selectedStudent;
        private List<Student> _reserveStudents;

        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                SetProperty(ref _selectedStudent, value);
            }
        }
        public ObservableCollection<Group> Groups { get; set; }

        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SaveItem { get; set; }

        public StudentsTabPageViewModel()
        {
            _serverApi = new ServerApi();
            Students = new ObservableCollection<Student>();
            _reserveStudents = new List<Student>();
            SelectedStudent = null;
            Groups = new ObservableCollection<Group>();

            AddItem = new Command<string>(OnAddItem);
            RemoveItem = new Command<string>(OnRemoveItem);
            SaveItem = new Command<string>(OnSaveChanges);

            LoadStudentsFromServer();
        }
        private async void LoadStudentsFromServer()
        {
            var students = await _serverApi.GetStudents(CancellationToken.None);
            if (students == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            students = students.OrderBy(x => x.Name);
            foreach (var student in students)
            {
                Students.Add(student);
                _reserveStudents.Add(new Student(student));
            }
            LoadGroupsFromServer();
        }
        private async void LoadGroupsFromServer()
        {
            var groups = await _serverApi.GetGroups(CancellationToken.None);
            if (groups == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            groups = groups.OrderBy(x => x.Name);
            foreach (var group in groups)
            {
                Groups.Add(group);
            }
        }

        private async void OnAddItem(string _)
        {
            var addWindow = new StudentAddWindow(Groups);
            addWindow.ShowDialog();
            if (addWindow.DialogResult == true)
            {
                var newStudent = addWindow.GetStudent();

                var result = await _serverApi.AddStudent(newStudent, CancellationToken.None);
                if (result == null)
                {
                    MessageBox.Show("Отправить данные на сервер не удалось :(", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Students.Add(result);
                _reserveStudents.Add(new Student(result));
            }
        }
        private async void OnRemoveItem(string _)
        {
            _reserveStudents.RemoveAt(Students.IndexOf(SelectedStudent));
            await _serverApi.RemoveStudent(SelectedStudent.Id, CancellationToken.None);
            Students.Remove(SelectedStudent);
        }

        private async void OnSaveChanges(string _)
        {
            if (SelectedStudent != null)
            {
                var currentIndex = Students.IndexOf(SelectedStudent);
                var reserveSubject = _reserveStudents[currentIndex];

                if (!SelectedStudent.Equals(reserveSubject))
                {
                    await _serverApi.ChangeStudent(SelectedStudent.Id, SelectedStudent, CancellationToken.None);
                    _reserveStudents[currentIndex] = new Student(SelectedStudent);
                }
            }
        }

    }
}
