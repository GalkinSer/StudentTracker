using StudentTrackerAdminClient.ViewModels.Basics;
using System.Windows.Controls;

namespace StudentTrackerAdminClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public int SelectedTabIndex { get; set; }
        public MainWindowViewModel()
        {
            SelectedTabIndex = -1;
        }

    }
}
