using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerAdminClient.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentTrackerAdminClient.ViewModels
{
    public class ItemSelectWindowViewModel : BaseViewModel
    {
        private readonly ItemSelectWindow _window;
        private bool _isOkEnabled;

        public List<object> Items { get; set; }
        public object SelectedItem { get; set; }
        public bool IsOkEnabled
        {
            get { return _isOkEnabled; }
            set
            {
                SetProperty(ref _isOkEnabled, value);
            }
        }

        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }

        public ItemSelectWindowViewModel()
        {
            Items = new List<object>();
            SelectedItem = null;
            _window = null;
            IsOkEnabled = false;

            Ok = new Command<string>(OnOk);
            Cancel = new Command<string>(OnCancel);

        }
        public ItemSelectWindowViewModel(IEnumerable<object> items, ItemSelectWindow window) : this()
        {
            Items = new List<object>(items);
            SelectedItem = null;
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
        public void OnSelectionChanged(object source, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
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
