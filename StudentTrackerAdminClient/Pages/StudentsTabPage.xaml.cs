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
    /// Логика взаимодействия для StudentsTabPage.xaml
    /// </summary>
    public partial class StudentsTabPage : Page
    {
        private readonly StudentsTabPageViewModel _viewModel;
        public StudentsTabPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new StudentsTabPageViewModel();
        }
    }
}
