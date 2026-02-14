using StudentTrackerClient.Controls;
using StudentTrackerClient.Services;
using StudentTrackerClient.ViewModels.Basics;
using StudentTrackerClient.Windows;
using StudentTrackerLib.Models;
using StudentTrackerLib.Models.Operational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace StudentTrackerClient.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region Fields
		private Teacher _currentTeacher;
		private Subject _currentSubject;
		private Group _currentGroup;

		public event Action<Header> HeaderAdded;
		public event Action<int> HeaderDeleted;
		public event Action HeadersCleared;
		public event Action<Header> HeaderEdited;
		private readonly ServerApi _serverApi;
		private int _headersCount;
		private bool _isTeacherPresent;
		private bool _isGroupSelected;
		private List<int> _deletedHeadersIds;
		#endregion

		#region Properties
		public Teacher CurrentTeacher
		{
			get => _currentTeacher;
			set
			{
				SetProperty(ref _currentTeacher, value);
				if (value is not null)
					IsTeacherPresent = true;
				else
					IsTeacherPresent = false;
				GetSubjects();
			}
		}
		public Subject CurrentSubject
		{
			get { return _currentSubject; }
			set
			{
				SetProperty(ref _currentSubject, value);
				GetGroups();
			}
		}
		public Group CurrentGroup
		{
			get { return _currentGroup; }
			set
			{
                SaveHeaders();
                SetProperty(ref _currentGroup, value);
				GetStudents();
				GetHeaders();
			}
		}

		public ObservableCollection<Student> Students { get; set; }
		public ObservableCollection<Group> Groups { get; set; }
		public ObservableCollection<Subject> Subjects { get; set; }
		public ObservableCollection<Header> Headers { get; set; }

		public ICommand SwitchTeacher { get; set; }
		public bool IsTeacherPresent
		{
			get => _isTeacherPresent;
			set => SetProperty(ref _isTeacherPresent, value);
		}
		#endregion

		#region Constructors
		public MainWindowViewModel()
		{
			HeaderAdded += Заглушка;
			HeadersCleared += Заглушка;
			HeaderDeleted += Заглушка;
			HeaderEdited += Заглушка;
			_serverApi = new ServerApi();
			_deletedHeadersIds = new List<int>();

			Headers = new ObservableCollection<Header>();
			CurrentTeacher = new Teacher();
			Groups = new ObservableCollection<Group>();
			Subjects = new ObservableCollection<Subject>();
			Students = new ObservableCollection<Student>();

			Headers.CollectionChanged += Headers_CollectionChanged;

			SwitchTeacher = new Command<string>(OnSwitchTeacher);
		}
		public MainWindowViewModel(Action<Header> addHeaderHandler, Action<int> deleteHeaderHandler,
			Action clearHeadersHandler) : this()
		{
			HeaderAdded -= Заглушка;
			HeaderAdded += addHeaderHandler;
			HeaderDeleted -= Заглушка;
			HeaderDeleted += deleteHeaderHandler;
			HeadersCleared -= Заглушка;
			HeadersCleared += clearHeadersHandler;
		}
		#endregion

		#region Methods
		private void Headers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
			{
				foreach (var item in e.NewItems)
				{
					if (item is Header Header)
					{
						HeaderAdded.Invoke(Header);
						_headersCount++;
					}
				}
			}
			//else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
			//{
			//	foreach (var item in e.OldItems)
			//	{
			//		if (item is Header Header)
			//		{
			//			HeaderDeleted.Invoke(Header.Id);
			//			_headersCount--;
			//		}
			//	}
			//}
			else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
			{
				HeadersCleared.Invoke();
			}
		}
		public void BlankHeaderChanged(Header changedHeader)
		{
			if (Headers.IndexOf(changedHeader) == Headers.Count-1 || Headers.Count==1)
			{
				Header blankHeader = new Header()
				{
					Id = Headers.Max(x => x.Id)+1,
					Group = CurrentGroup,
					Subject = CurrentSubject,
					Teacher = CurrentTeacher,
				};
				List<Mark> marksForBlankHeader = new List<Mark>();
				foreach (Student student in Students)
				{
					marksForBlankHeader.Add(new Mark() { Student = student, Header = blankHeader });
				}
				blankHeader.Marks = marksForBlankHeader;

				Headers.Add(blankHeader);
			}
		}
		public void DeleteHeader(int headerId)
		{
			Header headerToDelete = Headers.SingleOrDefault(x => x.Id == headerId);
			if (headerToDelete != null)
			{
				HeaderDeleted.Invoke(Headers.IndexOf(headerToDelete));
				Headers.Remove(headerToDelete);
				_deletedHeadersIds.Add(headerId);
			}
		}
		public void OpenAuthWindow(object? sender, EventArgs e)
		{
			var authWindow = new AuthenticationWindow(_serverApi);
			var dialogResult = authWindow.ShowDialog();
			//if (dialogResult.HasValue == true && dialogResult.Value == true)
			//{
				CurrentTeacher = authWindow.GetAuthenticatedTeacher();
			//}
		}

		private void Заглушка(Header _)
		{

		}
		private void Заглушка(int _)
		{

		}
		private void Заглушка()
		{

		}

		private void GetSubjects()
		{
			CurrentSubject = null;
			Subjects = new ObservableCollection<Subject>(_serverApi.GetSubjectsForTeacher(CurrentTeacher));
			InvokeOnPropertyChangedEvent(nameof(Subjects));
		}
		private void GetGroups()
		{
			CurrentGroup = null;
			Groups = new ObservableCollection<Group>(_serverApi.GetGroupsForSubject(CurrentSubject));
			InvokeOnPropertyChangedEvent(nameof(Groups));
		}
		private void GetStudents()
		{
			Students = new ObservableCollection<Student>(from student in _serverApi.GetStudentsForGroup(CurrentGroup) orderby student.Name select student);
			InvokeOnPropertyChangedEvent(nameof(Students));
		}
		private void GetHeaders()
		{
			List<Header> newHeaders = (_serverApi.GetHeaders(CurrentTeacher, CurrentGroup, CurrentSubject)).Select(x => new Header(x)).ToList();
			Headers.Clear();
			//int headerId = 0;
			foreach (var header in newHeaders)
			{
				List<Mark> marks = _serverApi.GetMarks(header);
				var nonPresentStudents = Students.Except(from mark in marks select mark.Student);
				foreach (Student student in nonPresentStudents)
				{
					marks.Add(new Mark() { Student = student, Header = header });
				}
				marks = (from mark in marks orderby mark.Student select mark).ToList();
				var completeHeader = new Header(marks)
				{
					Id = header.Id,
					Group = header.Group,
					Title = header.Title,
					Teacher = header.Teacher,
					Subject = header.Subject,
				};
				Headers.Add(completeHeader);
				//headerId++;
			}

			if (CurrentGroup != null)
			{
				Header blankHeader = new Header()
				{
					Group = CurrentGroup,
					Subject = CurrentSubject,
					Teacher = CurrentTeacher,
				};
				List<Mark> marksForBlankHeader = new List<Mark>();
				foreach (Student student in Students)
				{
					marksForBlankHeader.Add(new Mark() { Student = student, Header = blankHeader });
				}
				blankHeader.Marks = marksForBlankHeader;
				Headers.Add(blankHeader);
			}

			foreach (var header in Headers)
			{
				header.IsChanged = false;
			}
			#endregion
		}

		private void OnSwitchTeacher(string _)
		{
			CurrentTeacher = null;
			OpenAuthWindow(null, null);
		}

		private void SaveHeaders()
		{
			foreach (var header in Headers)
			{
				if (header.IsChanged)
				{
					var thisHeader = Headers.FirstOrDefault(x => x.Id == header.Id);
					if (thisHeader != null)
					{
						_serverApi.ChangeHeader(thisHeader.Id, header);
					}
					else
					{
						_serverApi.AddHeader(header);
					}
				}
			}
			foreach (int id in _deletedHeadersIds)
			{
				_serverApi.RemoveHeader(id);
			}
		}
		
	}
}
