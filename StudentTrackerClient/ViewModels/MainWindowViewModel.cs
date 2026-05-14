using StudentTrackerClient.Services;
using StudentTrackerClient.ViewModels.Basics;
using StudentTrackerClient.Windows;
using StudentTrackerLib;
using StudentTrackerLib.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace StudentTrackerClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private Teacher _currentTeacher;
        private Subject _currentSubject;
        private Group _currentGroup;

        public event Action<Header> HeaderAdded;
        public event Action<int> HeaderDeleted;
        public event Action HeadersCleared;
        public event Action<Header> HeaderEdited;
        private readonly ServerApi _serverApi;
        private List<Header> _reserveHeaders = new List<Header>();
        private List<int> _deletedHeadersIds = new List<int>();
        private List<int> _deletedMarksIds = new List<int>();
        private bool _isTeacherPresent;
        #endregion

        #region Properties
        public Teacher CurrentTeacher
        {
            get => _currentTeacher;
            set
            {
                SetProperty(ref _currentTeacher, value);
                if (value is not null)
                {
                    if (Headers.Count > 0)
                        SaveHeaders("");
                    Subjects.Clear();
                    Groups.Clear();
                    Students.Clear();
                    Headers.Clear();
                    GetSubjects();
                }
            }
        }
        public Subject CurrentSubject
        {
            get { return _currentSubject; }
            set
            {
                SetProperty(ref _currentSubject, value);
                if (value is not null)
                {
                    if (Headers.Count > 0)
                        SaveHeaders("");
                    Groups.Clear();
                    Students.Clear();
                    Headers.Clear();
                    GetGroups();
                }
            }
        }
        public Group CurrentGroup
        {
            get { return _currentGroup; }
            set
            {
                SetProperty(ref _currentGroup, value);
                if (value != null)
                {
                    if (Headers.Count > 0)
                        SaveHeaders("");
                    Students.Clear();
                    Headers.Clear();
                    _reserveHeaders.Clear();
                    GetTable();
                }
            }
        }
        public bool IsTeacherPresent
        {
            get { return _isTeacherPresent; }
            set
            {
                SetProperty(ref _isTeacherPresent, value);
            }
        }

        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Subject> Subjects { get; set; }
        public ObservableCollection<Header> Headers { get; set; }

        public ICommand Save { get; set; }
        public ICommand SwitchTeacher { get; set; }
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            HeaderAdded += Заглушка;
            HeadersCleared += Заглушка;
            HeaderDeleted += Заглушка;
            HeaderEdited += Заглушка;
            _serverApi = new ServerApi();
            IsTeacherPresent = false;

            Headers = new ObservableCollection<Header>();
            Teachers = new ObservableCollection<Teacher>();

            Groups = new ObservableCollection<Group>();
            Subjects = new ObservableCollection<Subject>();
            Students = new ObservableCollection<Student>();

            Save = new Command<string>(SaveHeaders);
            SwitchTeacher = new Command<string>(OnSwitchTeacher);

            Headers.CollectionChanged += Headers_CollectionChanged;
        }
        public MainWindowViewModel(Action<Header> addHeaderHandler, Action<int> deleteHeaderHandler,
            Action clearHeadersHandler) : this()
        {
            HeaderAdded -= Заглушка;
            HeaderAdded += addHeaderHandler;
            HeaderDeleted -= Заглушка;
            HeaderDeleted += deleteHeaderHandler;
            HeadersCleared -= Заглушка;
            HeadersCleared += clearHeadersHandler;
        }
        #endregion

        #region Methods
        private void Headers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is Header Header)
                    {
                        HeaderAdded.Invoke(Header);
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                HeadersCleared.Invoke();
            }
        }
        public void BlankHeaderChanged(Header changedHeader)
        {
            if (Headers.IndexOf(changedHeader) == Headers.Count - 1 || Headers.Count == 1)
            {
                Header blankHeader = new Header()
                {
                    Id = -1,
                    Group = CurrentGroup,
                    Subject = CurrentSubject,
                    Teacher = CurrentTeacher,
                };
                List<Mark> marksForBlankHeader = new List<Mark>();
                foreach (Student student in Students)
                {
                    marksForBlankHeader.Add(new Mark() { Student = student, Header = blankHeader });
                }
                blankHeader.Marks = marksForBlankHeader;
                _reserveHeaders.Add(new Header() { Id = -1 });
                Headers.Add(blankHeader);
            }
        }
        public void HeaderTitleIsEmpty(Header header)
        {
            if (header.Marks.Count == 0)
            {
                DeleteHeader(header.Id);
            }
            else
            {
                bool isMarkHasContent = false;
                foreach (Mark mark in header.Marks)
                {
                    if (mark.Content != null && mark.Content != "" && mark.Content != string.Empty)
                    {
                        isMarkHasContent = true;
                        break;
                    }
                }
                if (!isMarkHasContent)
                {
                    DeleteHeader(header.Id);
                }
            }
        }
        public void DeleteHeader(int headerId)
        {
            Header headerToDelete = Headers.FirstOrDefault(x => x.Id == headerId);
            if (headerToDelete != null)
            {
                int headerToDeleteIndex = Headers.IndexOf(headerToDelete);
                HeaderDeleted.Invoke(headerToDeleteIndex);
                Headers.Remove(headerToDelete);
                //_reserveHeaders.Remove(reserveHeaderToDelete);
                _reserveHeaders.RemoveAt(headerToDeleteIndex);
                _deletedHeadersIds.Add(headerId);
                foreach (Mark mark in headerToDelete.Marks)
                {
                    _deletedMarksIds.Add(mark.Id);
                }
            }
        }
        private void Заглушка(Header _)
        {

        }
        private void Заглушка(int _)
        {

        }
        private void Заглушка()
        {

        }

        private async void GetSubjects()
        {
            CurrentSubject = null;
            var subjects = await _serverApi.GetSubjects(CurrentTeacher.Id, CancellationToken.None);
            if (subjects == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Subjects = new ObservableCollection<Subject>(subjects);
            InvokeOnPropertyChangedEvent(nameof(Subjects));
        }
        private async void GetGroups()
        {
            CurrentGroup = null;
            var groups = await _serverApi.GetGroups(CurrentSubject.Id, CancellationToken.None);
            if (groups == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Groups = new ObservableCollection<Group>(groups);
            InvokeOnPropertyChangedEvent(nameof(Groups));
        }
        private async Task GetStudents()
        {
            var students = await _serverApi.GetStudents(CurrentGroup.Id, CancellationToken.None);
            if (students == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Students = new ObservableCollection<Student>(students);
            InvokeOnPropertyChangedEvent(nameof(Students));
        }
        private async Task GetHeaders()
        {
            var newHeaders = await _serverApi.GetHeaders
                (
                    CurrentTeacher.Id,
                    CurrentSubject.Id,
                    CurrentGroup.Id,
                    CancellationToken.None
                );
            if (newHeaders == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Headers.Clear();

            foreach (var header in newHeaders)
            {
                var marks = await _serverApi.GetMarks(header.Id, CancellationToken.None);
                if (marks == null)
                {
                    MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var marksList = marks.ToList();

                var nonPresentStudents = Students.ExceptCollection(marksList.Select(x => x.Student));
                foreach (Student student in nonPresentStudents)
                {
                    marksList.Add(new Mark() { Student = student, Header = header });
                }

                marksList = (from mark in marksList orderby mark.Student select mark).ToList();

                var completeHeader = new Header(marksList)
                {
                    Id = header.Id,
                    Group = header.Group,
                    Title = header.Title,
                    Teacher = header.Teacher,
                    Subject = header.Subject,
                };
                Headers.Add(completeHeader);
                _reserveHeaders.Add(new Header(completeHeader));
            }

            var blankHeaderId = 0;
            if (Headers.Count > 0)
                blankHeaderId = Headers.Max(x => x.Id) + 1;

            Header blankHeader = new Header()
            {
                Id = -1,
                Group = CurrentGroup,
                Subject = CurrentSubject,
                Teacher = CurrentTeacher,
            };
            List<Mark> marksForBlankHeader = new List<Mark>();
            foreach (Student student in Students)
            {
                marksForBlankHeader.Add(new Mark() { Student = student, Header = blankHeader });
            }
            blankHeader.Marks = marksForBlankHeader;
            Headers.Add(blankHeader);
        }
        private async void GetTable()
        {
            await GetStudents();
            await GetHeaders();
        }

        private async void SaveHeaders(string _)
        {
            var headersToSave = Headers.Take(Headers.Count - 1).ToList();
            var reserveHeaders = new List<Header>(_reserveHeaders);

            for (int i = 0; i < headersToSave.Count; i++)
            {
                Header header = headersToSave[i];
                Header reserveHeader = reserveHeaders[i];
                if (!header.Equals(reserveHeader))
                {
                    if (reserveHeader.Id == -1)
                    {
                        await _serverApi.AddHeader(header, CancellationToken.None);
                        foreach (var mark in header.Marks)
                        {
                            if (!string.IsNullOrEmpty(mark.Content))
                                await _serverApi.AddMark(mark, CancellationToken.None);
                        }
                        _reserveHeaders[i] = new Header(header);
                    }
                    else
                    {
                        if (!header.Title.Equals(reserveHeader.Title))
                            _serverApi.ChangeHeader(headersToSave[i].Id, headersToSave[i], CancellationToken.None);
                        for (int markI = 0; markI < header.Marks.Count; markI++)
                        {
                            if (string.IsNullOrEmpty(header.Marks[markI].Content))
                            {
                                _serverApi.RemoveMark(header.Marks[markI].Id, CancellationToken.None);
                            }
                            else if (!header.Marks[markI].Content.Equals(reserveHeader.Marks[markI]))
                            {
                                if (string.IsNullOrEmpty(reserveHeader.Marks[markI].Content))
                                    await _serverApi.AddMark(header.Marks[markI], CancellationToken.None);
                                else
                                    _serverApi.ChangeMark(header.Marks[markI].Id, header.Marks[markI], CancellationToken.None);
                            }
                        }
                        _reserveHeaders[i] = new Header(header);
                    }
                }
            }
            foreach (int id in _deletedHeadersIds)
            {
                _serverApi.RemoveHeader(id, CancellationToken.None);
            }
            foreach (int id in _deletedMarksIds)
            {
                _serverApi.RemoveMark(id, CancellationToken.None);
            }
        }

        private void OnSwitchTeacher(string _)
        {
            CurrentTeacher = null;
            IsTeacherPresent = false;
            OpenAuthWindow(null, null);
        }
        public void OpenAuthWindow(object? sender, EventArgs e)
        {
            var authWindow = new AuthenticationWindow(_serverApi);

            authWindow.ShowDialog();

            if (authWindow.DialogResult == true)
            {
                CurrentTeacher = authWindow.GetAuthenticatedTeacher();
                IsTeacherPresent = true;
            }
        }
        #endregion
    }
}
