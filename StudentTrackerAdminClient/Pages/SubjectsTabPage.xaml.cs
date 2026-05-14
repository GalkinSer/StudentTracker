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
    /// Логика взаимодействия для SubjectsTabPage.xaml
    /// </summary>
    public partial class SubjectsTabPage : Page
    {
        private readonly SubjectsTabPageViewModel _viewModel;
        public SubjectsTabPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new SubjectsTabPageViewModel();
        }
    }
}
