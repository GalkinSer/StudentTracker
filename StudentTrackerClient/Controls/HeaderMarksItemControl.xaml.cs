using StudentTrackerClient.ViewModels;
using StudentTrackerLib.Models.Operational;
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

namespace StudentTrackerClient.Controls
{
	/// <summary>
	/// Логика взаимодействия для HeaderMarksItemControl.xaml
	/// </summary>
	public partial class HeaderMarksItemControl : UserControl
	{
		private HeaderMarksItemControlViewModel _viewModel;
		private TextBox _markContentTextBox;
		public HeaderMarksItemControl()
		{
			InitializeComponent();
			DataContext = _viewModel = new HeaderMarksItemControlViewModel();
		}

		public HeaderMarksItemControl(HeaderMarksItemControlViewModel viewModel)
		{
			InitializeComponent();
			DataContext = _viewModel = viewModel;
			TitleTextBox.TextChanged += _viewModel.HeaderTextChanged;
            TitleTextBox.ContextMenu.Opened += _viewModel.ContextMenuOpenedHandler;
        }
        private void T_TextChanged(object sender, TextChangedEventArgs e)
        {
			((Mark)(_markContentTextBox.DataContext)).Content = _markContentTextBox.Text;
        }
    }
}
