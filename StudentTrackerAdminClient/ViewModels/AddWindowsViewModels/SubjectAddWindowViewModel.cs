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
    public class SubjectAddWindowViewModel : BaseViewModel
    {
        private readonly SubjectAddWindow? _window;
        private bool _isOkEnabled;

        public Subject Subject { get; set; }
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

        public SubjectAddWindowViewModel()
        {
            Subject = new Subject();
            _window = null;
            IsOkEnabled = false;

            Ok = new Command<string>(OnOk);
            Cancel = new Command<string>(OnCancel);
        }
        public SubjectAddWindowViewModel(SubjectAddWindow window) : this()
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
