using StudentTrackerClient.Controls;
using StudentTrackerClient.ViewModels;
using StudentTrackerLib.Models;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace StudentTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowViewModel _viewModel;
		public MainWindow()
		{
			DataContext = _viewModel = new MainWindowViewModel(AddHeaderControl, DeleteHeaderControl, ClearHeadersControls);
            this.Initialized += _viewModel.OpenAuthWindow;
            InitializeComponent();
			if (_viewModel.IsTeacherPresent == false)
				this.Close();
			HTSPScrollViewer.ScrollChanged += HTSPScrollViewer_ScrollChanged;
		}

        private void HTSPScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			SLICScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			
		}
		private void AddHeaderControl(Header header)
		{
			HeaderMarksItemControlViewModel viewModel = new HeaderMarksItemControlViewModel(header);
			viewModel.BlankHeaderChanged += _viewModel.BlankHeaderChanged;
			viewModel.HeaderTitleIsEmpty += _viewModel.HeaderTitleIsEmpty;
			viewModel.DeleteHeader += _viewModel.DeleteHeader;
			HeadersTableStackPanel.Children.Add(new HeaderMarksItemControl(viewModel) { MinWidth=50});
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