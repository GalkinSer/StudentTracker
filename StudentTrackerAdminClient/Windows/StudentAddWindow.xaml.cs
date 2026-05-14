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
    /// Логика взаимодействия для StudentAddWindow.xaml
    /// </summary>
    public partial class StudentAddWindow : Window
    {
        private readonly StudentAddWindowViewModel _viewModel;
        public StudentAddWindow()
        {
            InitializeComponent();
        }
        public StudentAddWindow(IEnumerable<Group> groups) : this()
        {
            DataContext = _viewModel = new StudentAddWindowViewModel(this, groups.ToList());
            StudentNameTextBox.TextChanged += _viewModel.OnNameTextChanged;
        }
        public Student GetStudent()
        {
            return _viewModel.Student;
        }
    }
}
