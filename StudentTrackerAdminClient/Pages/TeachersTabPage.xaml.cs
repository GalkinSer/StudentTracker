using StudentTrackerAdminClient.ViewModels.TabPagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentTrackerAdminClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeachersTabPage.xaml
    /// </summary>
    public partial class TeachersTabPage : Page
    {
        private readonly TeachersTabPageViewModel _viewModel;
        public TeachersTabPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new TeachersTabPageViewModel();
            MainListBox.SelectionChanged += _viewModel.MainListBox_SelectionChanged;
        }
    }
}
