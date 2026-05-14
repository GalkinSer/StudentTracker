using StudentTrackerAdminClient.ViewModels.TabPagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentTrackerAdminClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для GroupsTabPage.xaml
    /// </summary>
    public partial class GroupsTabPage : Page
    {
        private readonly GroupsTabPageViewModel _viewModel;
        public GroupsTabPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new GroupsTabPageViewModel();
            MainListBox.SelectionChanged += _viewModel.MainListBox_SelectionChanged;
        }
    }
}
