using StudentTrackerAdminClient.Services;
using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using StudentTrackerLib.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels.TabPagesViewModels
{
    public class SubjectsTabPageViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private Subject _selectedSubject;
        private List<Subject> _reserveSubjects;
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                SetProperty(ref _selectedSubject, value);
            }
        }

        public ICommand AddItem { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SaveItem { get; set; }

        public SubjectsTabPageViewModel()
        {
            _serverApi = new ServerApi();
            Subjects = new ObservableCollection<Subject>();
            _reserveSubjects = new List<Subject>();
            SelectedSubject = null;

            AddItem = new Command<string>(OnAddItem);
            RemoveItem = new Command<string>(OnRemoveItem);
            SaveItem = new Command<string>(OnSaveChanges);

            LoadSubjectsFromServer();
        }
        private async void LoadSubjectsFromServer()
        {
            var subjects = await _serverApi.GetSubjects(CancellationToken.None);
            if (subjects == null)
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            subjects = subjects.OrderBy(x => x.Name);
            foreach (var subject in subjects)
            {
                Subjects.Add(subject);
                _reserveSubjects.Add(new Subject(subject));
            }
        }

        private async void OnAddItem(string _)
        {
            var addWindow = new SubjectAddWindow();
            addWindow.ShowDialog();
            if (addWindow.DialogResult == true)
            {
                var newSubject = addWindow.GetSubject();
                var result = await _serverApi.AddSubject(newSubject, CancellationToken.None);
                if (result == null)
                {
                    MessageBox.Show("Отправить данные на сервер не удалось :(", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Subjects.Add(result);
                _reserveSubjects.Add(new Subject(result));
            }
        }
        private async void OnRemoveItem(string _)
        {
            _reserveSubjects.RemoveAt(Subjects.IndexOf(SelectedSubject));
            await _serverApi.RemoveSubject(SelectedSubject.Id, CancellationToken.None);
            Subjects.Remove(SelectedSubject);
        }

        private async void OnSaveChanges(string _)
        {
            if (SelectedSubject != null)
            {
                var currentIndex = Subjects.IndexOf(SelectedSubject);
                var reserveSubject = _reserveSubjects[currentIndex];

                if (!SelectedSubject.Equals(reserveSubject))
                {
                    await _serverApi.ChangeSubject(SelectedSubject.Id, SelectedSubject, CancellationToken.None);
                    _reserveSubjects[currentIndex] = new Subject(SelectedSubject);
                }
            }
        }
    }
}
