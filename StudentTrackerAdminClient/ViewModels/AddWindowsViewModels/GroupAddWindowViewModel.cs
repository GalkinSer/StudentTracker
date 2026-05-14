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
    public class GroupAddWindowViewModel : BaseViewModel
    {
        private readonly ServerApi _serverApi;
        private readonly GroupAddWindow? _window;
        private bool _isOkEnabled;
        private Subject _selectedSubject;

        public Group Group { get; set; }
        public bool IsOkEnabled
        {
            get { return _isOkEnabled; }
            set
            {
                SetProperty(ref _isOkEnabled, value);
            }
        }
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                SetProperty(ref _selectedSubject, value);
            }
        }

        public ICommand Ok {  get; set; }
        public ICommand Cancel { get; set; }
        public ICommand AddSubject { get; set; }
        public ICommand DeleteSubject { get; set; }


        public GroupAddWindowViewModel()
        {
            Group = new Group();
            Subjects = new ObservableCollection<Subject>();
            _serverApi = new ServerApi();
            _window = null;
            IsOkEnabled = false;

            Ok = new Command<string>(OnOk);
            Cancel = new Command<string>(OnCancel);
            AddSubject = new Command<string>(OnAddSubject);
            DeleteSubject = new Command<string>(OnDeleteSubject);
        }
        public GroupAddWindowViewModel(GroupAddWindow window) : this()
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

        public Group GetGroup()
        {
            foreach (var subject in Subjects)
            {
                Group.Subjects.Add(subject);
            }

            return Group;
        }
    }
}
