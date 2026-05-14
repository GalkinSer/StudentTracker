using StudentTrackerAdminClient.Controls;
using StudentTrackerAdminClient.ViewModels;
using StudentTrackerAdminClient.ViewModels.TabPagesViewModels;
using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для MarkTableTabPage.xaml
    /// </summary>
    public partial class MarkTableTabPage : Page
    {
        private readonly MarkTableTabPageViewModel _viewModel;
        public MarkTableTabPage()
        {
            InitializeComponent();
            DataContext = _viewModel = new MarkTableTabPageViewModel
                (
                    AddHeaderControl,
                    DeleteHeaderControl,
                    ClearHeadersControls
                );
            HTSPScrollViewer.ScrollChanged += HTSPScrollViewer_ScrollChanged;
        }
        private void HTSPScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            SLICScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
        }
        private void AddHeaderControl(Header header)
        {
            HeaderMarksItemControlViewModel viewModel = new HeaderMarksItemControlViewModel(header);
            viewModel.BlankHeaderChanged += _viewModel.BlankHeaderChanged;
            viewModel.HeaderTitleIsEmpty += _viewModel.HeaderTitleIsEmpty;
            viewModel.DeleteHeader += _viewModel.DeleteHeader;
            HeadersTableStackPanel.Children.Add(new HeaderMarksItemControl(viewModel) { MinWidth = 50 });
        }
        private void DeleteHeaderControl(int index)
        {
            HeadersTableStackPanel.Children.RemoveAt(index);
        }
        private void ClearHeadersControls()
        {
            HeadersTableStackPanel.Children.Clear();
        }

    }
}
