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
using StudentTrackerClient.ViewModels;
using StudentTrackerLib.Models.Operational;

namespace StudentTrackerClient.Controls
{
	/// <summary>
	/// Логика взаимодействия для MarkItemControl.xaml
	/// </summary>
	public partial class MarkItemControl : UserControl
	{
		public MarkItemControl()
		{
			InitializeComponent();
            ContentTextBox.TextChanged += ContentTextBox_TextChanged;
		}

        private void ContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Mark)DataContext).Content = ContentTextBox.Text;
            ((Mark)DataContext).Header.IsChanged = true;
        }
	}
}
