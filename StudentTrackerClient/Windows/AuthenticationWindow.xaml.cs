using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using StudentTrackerClient.Services;
using StudentTrackerClient.ViewModels;
using StudentTrackerLib.Models;

namespace StudentTrackerClient.Windows
{
	/// <summary>
	/// Логика взаимодействия для AuthenticationWindow.xaml
	/// </summary>
	public partial class AuthenticationWindow : Window
	{
		private AuthenticationWindowViewModel _viewModel;
		public AuthenticationWindow()
		{
			InitializeComponent();
		}
		internal AuthenticationWindow(ServerApi serverApi) : this()
		{
			DataContext = _viewModel = new AuthenticationWindowViewModel(serverApi, this);
			PasswordBox.PasswordChanged += _viewModel.PasswordChanged;
		}

		internal Teacher? GetAuthenticatedTeacher()
		{
			return _viewModel.AuthenticatedTeacher;
		}
	}
}
