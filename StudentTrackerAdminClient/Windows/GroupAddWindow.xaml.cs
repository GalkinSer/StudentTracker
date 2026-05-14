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
    /// Логика взаимодействия для GroupAddWindow.xaml
    /// </summary>
    public partial class GroupAddWindow : Window
    {
        private readonly GroupAddWindowViewModel _viewModel;
        public GroupAddWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new GroupAddWindowViewModel(this);
            GroupNameTextBox.TextChanged += _viewModel.OnNameTextChanged;
        }

        public Group GetGroup()
        {
            return _viewModel.GetGroup();
        }
    }
}
