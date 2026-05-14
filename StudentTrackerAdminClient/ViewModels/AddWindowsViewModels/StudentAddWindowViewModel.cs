using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels.AddWindowsViewModels
{
    public class StudentAddWindowViewModel : BaseViewModel
    {
        private readonly StudentAddWindow? _window;
        private bool _isOkEnabled;

        public Student Student { get; set; }
        public List<Group> Groups { get; set; }
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

        public StudentAddWindowViewModel()
        {
            Student = new Student();
            _window = null;
            IsOkEnabled = false;

            Ok = new Command<string>(OnOk);
            Cancel = new Command<string>(OnCancel);
        }
        public StudentAddWindowViewModel(StudentAddWindow window, List<Group> groups) : this()
        {
            _window = window;
            Groups = groups;
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
    }
}
