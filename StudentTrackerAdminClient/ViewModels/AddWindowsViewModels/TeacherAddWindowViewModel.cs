using StudentTrackerAdminClient.Services;
using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels.AddWindowsViewModels
{
    public class TeacherAddWindowViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private readonly TeacherAddWindow? _window;
        private bool _isOkEnabled;
        private Subject _selectedSubject;

        public Teacher Teacher { get; set; }
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                SetProperty(ref _selectedSubject, value);
            }
        }
        public bool IsOkEnabled
        {
            get { return _isOkEnabled; }
            set
            {
                SetProperty(ref _isOkEnabled, value);
            }
        }
        public ICommand Ok {  get; set; }
        public ICommand Cancel { get; set; }
        public ICommand AddSubject { get; set; }
        public ICommand DeleteSubject {  get; set; }

        public TeacherAddWindowViewModel()
        {
            Teacher = new Teacher();
            Subjects = new ObservableCollection<Subject>();
            _serverApi = new ServerApi();
            _window = null;
            IsOkEnabled = false;

            Ok = new Command<string>(OnOk);
            Cancel = new Command<string>(OnCancel);
            AddSubject = new Command<string>(OnAddSubject);
            DeleteSubject = new Command<string>(OnDeleteSubject);
        }
        public TeacherAddWindowViewModel(TeacherAddWindow window) : this()
        {
            _window = window;
        }

        private void OnOk(string _)
        {
            if (_window != null)
            {
                _window.DialogResult = true;
                _window.Close();
                return;
            }
            throw new Exception("Window is null (how tf you clicked this without window?)");
        }
        private void OnCancel(string _)
        {
            if (_window != null)
            {
                _window.DialogResult = false;
                _window.Close();
                return;
            }
            throw new Exception("Window is null (how tf you clicked this without window?)");
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
                Subjects.Add(newSubject);
            }
        }
        private void OnDeleteSubject(string _)
        {
            Subjects.Remove(SelectedSubject);
        }

        public void OnNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                IsOkEnabled = true;
            }
            else
            {
                IsOkEnabled = false;
            }
        }
        public Teacher GetTeacher()
        {
            if (_window != null)
            {
                var password = _window.GetPassword();
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                var passwordHash = Convert.ToHexString(SHA256.HashData(bytes));
                Teacher.PasswordHash = passwordHash;

                foreach (var subject in Subjects)
                {
                    Teacher.Subjects.Add(subject);
                }

                return Teacher;
            }
            throw new Exception("Window is null");
        }
    }
}
