using StudentTrackerAdminClient.ViewModels;
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
    /// Логика взаимодействия для ItemSelectWindow.xaml
    /// </summary>
    public partial class ItemSelectWindow : Window
    {
        private readonly ItemSelectWindowViewModel _viewModel;
        public ItemSelectWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new ItemSelectWindowViewModel();
        }
        public ItemSelectWindow(IEnumerable<object> items) : this()
        {
            DataContext = _viewModel = new ItemSelectWindowViewModel(items, this);
            MainComboBox.SelectionChanged += _viewModel.OnSelectionChanged;
        }
        public object GetSelectedItem()
        {
            return _viewModel.SelectedItem;
        }
    }
}
