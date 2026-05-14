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
    /// Логика взаимодействия для TeacherAddWindow.xaml
    /// </summary>
    public partial class TeacherAddWindow : Window
    {
        private readonly TeacherAddWindowViewModel _viewModel;
        public TeacherAddWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new TeacherAddWindowViewModel(this);
            TeacherNameTextBox.TextChanged += _viewModel.OnNameTextChanged;
        }
        public Teacher GetTeacher()
        {
            return _viewModel.GetTeacher();
        }
        internal string GetPassword()
        {
            return TeacherPasswordPasswordBox.Password;
        }
    }
}
