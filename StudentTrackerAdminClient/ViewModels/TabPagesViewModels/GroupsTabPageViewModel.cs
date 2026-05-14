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
    public class GroupsTabPageViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private Group _selectedGroup;
        private Student _selectedStudent;
        private Subject _selectedSubject;
        private bool _isGroupSelected;
        private List<Group> _reserveGroups;
        private List<int> _deletedSubjectIds;
        private List<int> _addedSubjectIds;
        private List<Student> _addedStudents;

        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Student> GroupsStudents { get; set; }
        public ObservableCollection<Subject> GroupsSubjects { get; set; }
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                SetProperty(ref _selectedGroup, value);
            }
        }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                SetProperty(ref _selectedStudent, value);
            }
        }
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                SetProperty(ref _selectedSubject, value);
            }
        }

        public bool IsGroupSelected
        {
            get { return _isGroupSelected; }
            set
            {
                SetProperty(ref _isGroupSelected, value);
            }
        }

        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SaveItem { get; set; }


        public ICommand AddStudent { get; set; }
        public ICommand AddSubject { get; set; }
        public ICommand RemoveSubject { get; set; }

        public GroupsTabPageViewModel()
        {
            _serverApi = new ServerApi();
            Groups = new ObservableCollection<Group>();
            GroupsStudents = new ObservableCollection<Student>();
            GroupsSubjects = new ObservableCollection<Subject>();
            _addedStudents = new List<Student>();
            _addedSubjectIds = new List<int>();
            _deletedSubjectIds = new List<int>();
            _reserveGroups = new List<Group>();
            SelectedGroup = null;
            SelectedStudent = null;
            SelectedSubject = null;
            IsGroupSelected = false;

            AddItem = new Command<string>(OnAddItem);
            RemoveItem = new Command<string>(OnRemoveItem);
            SaveItem = new Command<string>(OnSaveChanges);
            AddStudent = new Command<string>(OnAddStudent);
            AddSubject = new Command<string>(OnAddSubject);
            RemoveSubject = new Command<string>(OnRemoveSubject);


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
                _reserveGroups.Add(new Group(group));
            }
        }

        private async void OnAddItem(string _)
        {
            var addWindow = new GroupAddWindow();
            addWindow.ShowDialog();
            if (addWindow.DialogResult == true)
            {
                var newGroup = addWindow.GetGroup();

                var result = await _serverApi.AddGroup(newGroup, CancellationToken.None);
                if (result == null)
                {
                    MessageBox.Show("Отправить данные на сервер не удалось :(", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var subject in newGroup.Subjects)
                {
                    _serverApi.AssignSubjectForGroup
                        (result.Id, subject.Id, CancellationToken.None);
                    result.Subjects.Add(subject);
                }
                foreach (var student in newGroup.Students)
                {
                    student.Group = SelectedGroup;
                    await _serverApi.ChangeStudent
                        (student.Id, student, CancellationToken.None);
                    result.Students.Add(student);
                }

                Groups.Add(result);
                _reserveGroups.Add(new Group(result));
            }
        }
        private async void OnRemoveItem(string _)
        {
            _reserveGroups.RemoveAt(Groups.IndexOf(SelectedGroup));
            await _serverApi.RemoveGroup(SelectedGroup.Id, CancellationToken.None);
            Groups.Remove(SelectedGroup);
        }

        private async void OnAddStudent(string _)
        {
            var students = await _serverApi.GetStudents(CancellationToken.None);
            if (students == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var itemSelectWindow = new ItemSelectWindow(students);
            itemSelectWindow.ShowDialog();
            if (itemSelectWindow.DialogResult == true)
            {
                var newStudent = (Student)itemSelectWindow.GetSelectedItem();
                SelectedGroup.Students.Add(newStudent);
                GroupsStudents.Add(newStudent);
                _addedStudents.Add(newStudent);
            }
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
                SelectedGroup.Subjects.Add(newSubject);
                GroupsSubjects.Add(newSubject);
                _addedSubjectIds.Add(newSubject.Id);
            }
        }
        private void OnRemoveSubject(string _)
        {
            if (SelectedSubject == null) return;
            if (!_addedSubjectIds.Contains(SelectedSubject.Id))
                _deletedSubjectIds.Add(SelectedSubject.Id);
            else
                _addedSubjectIds.Remove(SelectedSubject.Id);
            SelectedGroup.Subjects.Remove(SelectedSubject);
            GroupsSubjects.Remove(SelectedSubject);
        }


        public void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupsStudents.Clear();
            GroupsSubjects.Clear();
            _addedSubjectIds.Clear();
            _deletedSubjectIds.Clear();
            _addedStudents.Clear();

            if (SelectedGroup == null)
            {
                IsGroupSelected = false;
                return;
            }
            else
            {
                IsGroupSelected = true;
                foreach (var student in SelectedGroup.Students)
                {
                    GroupsStudents.Add(student);
                }
                foreach (var subject in SelectedGroup.Subjects)
                {
                    GroupsSubjects.Add(subject);
                }
            }
        }
        private async void OnSaveChanges(string _)
        {
            if (SelectedGroup != null)
            {
                var currentIndex = Groups.IndexOf(SelectedGroup);
                var reserveGroup = _reserveGroups[currentIndex];

                bool areGroupsEqual = true;
                if (!SelectedGroup.Equals(reserveGroup))
                {
                    areGroupsEqual = false;
                }
                if (reserveGroup.Subjects.Count != SelectedGroup.Subjects.Count)
                    areGroupsEqual = false;
                for (int i = 0; i < reserveGroup.Subjects.Count; i++)
                {
                    if (!areGroupsEqual) break;
                    if (!reserveGroup.Subjects[i].Equals(SelectedGroup.Subjects[i]))
                    {
                        areGroupsEqual = false;
                        break;
                    }
                }

                if (reserveGroup.Students.Count != SelectedGroup.Students.Count)
                    areGroupsEqual = false;
                for (int i = 0; i < reserveGroup.Students.Count; i++)
                {
                    if (!areGroupsEqual) break;
                    if (!reserveGroup.Students[i].Equals(SelectedGroup.Students[i]))
                    {
                        areGroupsEqual = false;
                        break;
                    }
                }

                if (!areGroupsEqual)
                {

                    await _serverApi.ChangeGroup(SelectedGroup.Id, SelectedGroup, CancellationToken.None);
                    foreach (var subjectId in _deletedSubjectIds)
                    {
                        _serverApi.DeassignSubjectForGroup
                            (SelectedGroup.Id, subjectId, CancellationToken.None);
                    }
                    foreach (var subjectId in _addedSubjectIds)
                    {
                        _serverApi.AssignSubjectForGroup
                            (SelectedGroup.Id, subjectId, CancellationToken.None);
                    }
                    //foreach (var studentId in _deletedStudentIds)
                    //{
                    //    _serverApi.DeassignSubjectForGroup
                    //        (SelectedGroup.Id, studentId, CancellationToken.None);
                    //}
                    foreach (var student in _addedStudents)
                    {
                        student.Group = SelectedGroup;
                        await _serverApi.ChangeStudent
                            (student.Id, student, CancellationToken.None);
                    }
                    _reserveGroups[currentIndex] = new Group(SelectedGroup);

                }
            }
        }
    }
}
