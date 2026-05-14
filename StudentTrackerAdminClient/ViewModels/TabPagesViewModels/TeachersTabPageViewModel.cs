using StudentTrackerAdminClient.Services;
using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using StudentTrackerLib.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels.TabPagesViewModels
{
    public class TeachersTabPageViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private Teacher _selectedTeacher;
        //private Teacher _previouslySelectedTeacher;
        //private int _previouslySelectedTeacherIndex;
        private List<Teacher> _reserveTeachers;
        private Subject _selectedSubject;
        private List<int> _deletedSubjectIds;
        private List<int> _addedSubjectIds;
        private bool _isTeacherSelected;
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Subject> TeachersSubjects { get; set; }
        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                SetProperty(ref _selectedTeacher, value);
            }
        }
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                SetProperty(ref _selectedSubject, value);
            }
        }
        public bool IsTeacherSelected
        {
            get => _isTeacherSelected;
            set
            {
                SetProperty(ref _isTeacherSelected, value);
            }
        }

        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SaveItem { get; set; }
        public ICommand AddSubject { get; set; }
        public ICommand RemoveSubject { get; set; }

        public TeachersTabPageViewModel()
        {
            _serverApi = new ServerApi();
            Teachers = new ObservableCollection<Teacher>();
            _reserveTeachers = new List<Teacher>();
            //_previouslySelectedTeacher = null;
            //_previouslySelectedTeacherIndex = -1;
            TeachersSubjects = new ObservableCollection<Subject>();
            SelectedTeacher = null;
            SelectedSubject = null;
            IsTeacherSelected = false;
            _deletedSubjectIds = new List<int>();
            _addedSubjectIds = new List<int>();

            AddItem = new Command<string>(OnAddItem);
            RemoveItem = new Command<string>(OnRemoveItem);
            SaveItem = new Command<string>(OnSaveChanges);
            AddSubject = new Command<string>(OnAddSubject);
            RemoveSubject = new Command<string>(OnRemoveSubject);

            LoadTeachersFromServer();
        }
        private async void LoadTeachersFromServer()
        {
            var teachers = await _serverApi.GetTeachers(CancellationToken.None);
            if (teachers == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            teachers = teachers.OrderBy(x => x.Name);
            foreach (var teacher in teachers)
            {
                Teachers.Add(teacher);
                _reserveTeachers.Add(new Teacher(teacher));
            }
        }
        private async void OnAddItem(string _)
        {
            var addWindow = new TeacherAddWindow();
            addWindow.ShowDialog();
            if (addWindow.DialogResult == true)
            {
                var newTeacher = addWindow.GetTeacher();
                var result = await _serverApi.AddTeacher(newTeacher, CancellationToken.None);
                if (result == null)
                {
                    MessageBox.Show("Отправить данные на сервер не удалось :(", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var subject in newTeacher.Subjects)
                {
                    _serverApi.AssignSubjectForTeacher(result.Id, subject.Id, CancellationToken.None);
                    result.Subjects.Add(subject);
                }
                Teachers.Add(result);
                _reserveTeachers.Add(new Teacher(result));
            }
        }
        private async void OnRemoveItem(string _)
        {
            _reserveTeachers.RemoveAt(Teachers.IndexOf(SelectedTeacher));
            await _serverApi.RemoveTeacher(SelectedTeacher.Id, CancellationToken.None);
            Teachers.Remove(SelectedTeacher);
        }

        private async void OnAddSubject(string _)
        {
            var subjects = await _serverApi.GetSubjects(CancellationToken.None);
            if (subjects == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var itemSelectWindow = new ItemSelectWindow(subjects);
            itemSelectWindow.ShowDialog();
            if (itemSelectWindow.DialogResult == true)
            {
                var newSubject = (Subject)itemSelectWindow.GetSelectedItem();
                SelectedTeacher.Subjects.Add(newSubject);
                TeachersSubjects.Add(newSubject);
                _addedSubjectIds.Add(newSubject.Id);
            }
        }
        private void OnRemoveSubject(string _)
        {
            if (SelectedTeacher == null) return;
            SelectedTeacher.Subjects.Remove(SelectedSubject);
            if (!_addedSubjectIds.Contains(SelectedSubject.Id))
                _deletedSubjectIds.Add(SelectedSubject.Id);
            else
                _addedSubjectIds.Remove(SelectedSubject.Id);
            TeachersSubjects.Remove(SelectedSubject);
        }

        public void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeachersSubjects.Clear();
            _addedSubjectIds.Clear();
            _deletedSubjectIds.Clear();
            if (SelectedTeacher == null)
            {
                IsTeacherSelected = false;
                return;
            }
            else
            {
                IsTeacherSelected = true;
                foreach (var subject in SelectedTeacher.Subjects)
                {
                    TeachersSubjects.Add(subject);
                }
            }
        }

        private async void OnSaveChanges(string _)
        {
            if (SelectedTeacher != null)
            {
                var currentIndex = Teachers.IndexOf(SelectedTeacher);
                var reserveTeacher = _reserveTeachers[currentIndex];

                bool areTeachersEqual = true;
                if (!SelectedTeacher.Equals(reserveTeacher))
                {
                    areTeachersEqual = false;
                }
                if (reserveTeacher.Subjects.Count != SelectedTeacher.Subjects.Count)
                    areTeachersEqual = false;
                for (int i = 0; i < reserveTeacher.Subjects.Count; i++)
                {
                    if (!areTeachersEqual) break;
                    if (!reserveTeacher.Subjects[i].Equals(SelectedTeacher.Subjects[i]))
                    {
                        areTeachersEqual = false;
                        break;
                    }
                }

                if (!areTeachersEqual)
                {
                    await _serverApi.ChangeTeacher(SelectedTeacher.Id, SelectedTeacher, CancellationToken.None);
                    foreach (var subjectId in _deletedSubjectIds)
                    {
                        _serverApi.DeassignSubjectForTeacher(SelectedTeacher.Id, subjectId, CancellationToken.None);
                    }
                    foreach (var subjectId in _addedSubjectIds)
                    {
                        _serverApi.AssignSubjectForTeacher(SelectedTeacher.Id, subjectId, CancellationToken.None);
                    }
                    _reserveTeachers[currentIndex] = new Teacher(SelectedTeacher);
                }
            }
        }
    }
}
