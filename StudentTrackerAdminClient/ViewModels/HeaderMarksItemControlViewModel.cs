using StudentTrackerAdminClient.ViewModels.Basics;
using StudentTrackerLib.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace StudentTrackerAdminClient.ViewModels
{
	public class HeaderMarksItemControlViewModel : BaseViewModel
	{
		public Header Header { get; set; }
		public ICommand DeleteHeaderCommand { get; set; }
		public event Action<Header> BlankHeaderChanged;
		public event Action<Header> HeaderTitleIsEmpty;
		public event Action<int> DeleteHeader;

		public HeaderMarksItemControlViewModel() 
		{
            Header = new Header();
			DeleteHeaderCommand = new Command<string>(OnDeleteHeader);
		}
		public HeaderMarksItemControlViewModel(Header header) : this()
		{
            Header = header;
		}

		public void HeaderTextChanged(object sender, TextChangedEventArgs e)
		{
			if (Header.Title is null || Header.Title == "" || Header.Title == string.Empty)
				BlankHeaderChanged.Invoke(Header);
            Header.Title = ((TextBox)sender).Text;
            if (Header.Title is null || Header.Title == "" || Header.Title == string.Empty)
                HeaderTitleIsEmpty.Invoke(Header);
        }

        private void OnDeleteHeader(string _)
		{
			DeleteHeader.Invoke(Header.Id);
		}
		public void ContextMenuOpenedHandler(object sender, RoutedEventArgs e)
		{
			if (Header.Title is null || Header.Title == "" || Header.Title == string.Empty)
				((ContextMenu)sender).IsOpen = false;
			return;
		}
	}
}
