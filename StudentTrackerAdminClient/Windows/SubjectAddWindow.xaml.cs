using StudentTrackerAdminClient.ViewModels.AddWindowsViewModels;
using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentTrackerAdminClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для SubjectAddWindow.xaml
    /// </summary>
    public partial class SubjectAddWindow : Window
    {
        private readonly SubjectAddWindowViewModel _viewModel;
        public SubjectAddWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new SubjectAddWindowViewModel(this);
            SubjectNameTextBox.TextChanged += _viewModel.OnNameTextChanged;
        }
        public Subject GetSubject()
        {
            return _viewModel.Subject;
        }
    }
}
