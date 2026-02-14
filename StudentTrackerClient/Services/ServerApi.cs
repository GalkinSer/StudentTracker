using StudentTrackerLib.Models;
using StudentTrackerLib.Models.Operational;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerClient.Services
{
	internal class ServerApi
	{
		#region Debug properties
		private List<Teacher> AllTeachers { get; set; }
		private List<Student> AllStudents { get; set; }
		private List<Group> AllGroups { get; set; }
		private List<Subject> AllSubjects { get; set; }
		private List<Header> AllHeaders { get; set; }
		private List<Studies> AllStudies { get; set; }
		private List<Teaches> AllTeaches { get; set; }
		private List<Mark> AllMarks { get; set; }
		#endregion


		private readonly Uri _serverUri;

		internal ServerApi()
		{
			_serverUri = new Uri("https://localhost:5124/api");

			AllTeachers = new List<Teacher>()
			{
				new Teacher() { Id=0, Name="Смирнов Ярослав Александрович",/*Яся*/ PasswordHash="2e72a8187891d402dfeac6a84e547b2a433b1ad99a8aad913db6bb013da4e114".ToUpper()},
				new Teacher() { Id=1, Name="Бурганов Виталий Олегович",/*Кошка*/ PasswordHash="5b52b1b54397a25e52e8fa5325dc66e0619ed28b5a2210db30544abb5592b2db".ToUpper()},
				new Teacher() { Id=2, Name="Якимова Ольга Павловна",/*Пароль*/ PasswordHash = "cb1a2074b3a027ffa7d7d9c54682c3835fffc7f6d620d8a38532f075cc2f17a0".ToUpper()}
			};

			AllSubjects = new List<Subject>()
			{
				new Subject() { Id=0, Name="Электроника и схемотехника"},
				new Subject() { Id=1, Name="Системы управления базами данных"},
				new Subject() { Id=2, Name="Разработка приложений на платформе .NET"},
				new Subject() { Id=3, Name="Основы российской государственности"},
				new Subject() { Id=4, Name="История России с начала XIX века"},
				new Subject() { Id=5, Name="Основы объектно-ориентированного программирования"},
				new Subject() { Id=6, Name="Безопасность операционных систем"},
				new Subject() { Id=7, Name="Основы компьютерной безопасности"},
			};

			AllTeaches = new List<Teaches>()
			{
				new Teaches() { Teacher=AllTeachers[0], Subject=AllSubjects[3]},
				new Teaches() { Teacher=AllTeachers[0], Subject=AllSubjects[4]},
				new Teaches() { Teacher=AllTeachers[1], Subject=AllSubjects[0]},
				new Teaches() { Teacher=AllTeachers[1], Subject=AllSubjects[1]},
				new Teaches() { Teacher=AllTeachers[1], Subject=AllSubjects[5]},
				new Teaches() { Teacher=AllTeachers[2], Subject=AllSubjects[5]},
				new Teaches() { Teacher=AllTeachers[2], Subject=AllSubjects[6]},
				new Teaches() { Teacher=AllTeachers[2], Subject=AllSubjects[7]},
			};

			AllGroups = new List<Group>()
			{
				new Group() { Id=0, Name="ИБ-31"},
				new Group() { Id=1, Name="ПМИ-22"},
				new Group() { Id=2, Name="КБ-31"},
				new Group() { Id=3, Name="КБ-41"}
			};

			AllStudents = new List<Student>()
			{
				new Student() {Id = 0, Name="Галкин Сергей Иванович", Group=AllGroups[0], IsRepresentative=false, StudentCardNumber=100000},
				new Student() {Id = 1, Name="Молев Никита Иванович", Group=AllGroups[0], IsRepresentative=false, StudentCardNumber=200000},
				new Student() {Id = 2, Name="Картавенко Вячеслав Владимирович", Group=AllGroups[0], IsRepresentative=true, StudentCardNumber=300000},
				
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				/*new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},
				new Student() {Id = 3, Name="Хз))", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=400000},
				new Student() {Id = 4, Name="Кто-нибудь", Group=AllGroups[1], IsRepresentative=false, StudentCardNumber=500000},
				new Student() {Id = 5, Name="Рандомный студент", Group=AllGroups[1], IsRepresentative=true, StudentCardNumber=600000},*/

				new Student() {Id = 6, Name="Шемягин Михаил Александрович", Group=AllGroups[2], IsRepresentative=true, StudentCardNumber=700000},
				new Student() {Id = 7, Name="Вьялкин Матвей", Group=AllGroups[2], IsRepresentative=false, StudentCardNumber=800000},
				new Student() {Id = 8, Name="Саня", Group=AllGroups[2], IsRepresentative=false, StudentCardNumber=900000},
				
				new Student() {Id = 9, Name="Гурлева Алёна Юрьевна", Group=AllGroups[3], IsRepresentative=true, StudentCardNumber=110000},
				new Student() {Id = 10, Name="Ваня", Group=AllGroups[3], IsRepresentative=false, StudentCardNumber=120000},
				new Student() {Id = 11, Name="И ещё кто-нибудь", Group=AllGroups[3], IsRepresentative=false, StudentCardNumber=130000},
			};

			AllStudies = new List<Studies>()
			{
				new Studies() { Group=AllGroups[0], Subject=AllSubjects[0]},
				new Studies() { Group=AllGroups[0], Subject=AllSubjects[2]},
				new Studies() { Group=AllGroups[0], Subject=AllSubjects[5]},

				new Studies() { Group=AllGroups[1], Subject=AllSubjects[3]},
				new Studies() { Group=AllGroups[1], Subject=AllSubjects[4]},
				new Studies() { Group=AllGroups[1], Subject=AllSubjects[6]},


				new Studies() { Group=AllGroups[2], Subject=AllSubjects[0]},
				new Studies() { Group=AllGroups[2], Subject=AllSubjects[1]},
				new Studies() { Group=AllGroups[2], Subject=AllSubjects[7]},

				new Studies() { Group=AllGroups[3], Subject=AllSubjects[1]},
				new Studies() { Group=AllGroups[3], Subject=AllSubjects[2]},
				new Studies() { Group=AllGroups[3], Subject=AllSubjects[4]},

			};

			AllHeaders = new List<Header>()
			{
				new Header()
				{ Id=0, Subject=AllSubjects[3], Teacher=AllTeachers[0], Group = AllGroups[1],
					Title="02.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=1, Subject=AllSubjects[4], Teacher=AllTeachers[0], Group = AllGroups[1],
					Title="02.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=2, Subject=AllSubjects[3], Teacher=AllTeachers[0], Group = AllGroups[1],
					Title="Коллоквиум №1", Marks=new List<Mark>()},
				new Header()
				{ Id=3, Subject=AllSubjects[3], Teacher=AllTeachers[0], Group = AllGroups[1],
					Title="Зачёт", Marks=new List<Mark>()},
				new Header()
				{ Id=4, Subject=AllSubjects[4], Teacher=AllTeachers[0], Group = AllGroups[3],
					Title="09.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=5, Subject=AllSubjects[4], Teacher=AllTeachers[0], Group = AllGroups[3],
					Title="16.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=6, Subject=AllSubjects[4], Teacher=AllTeachers[0], Group = AllGroups[1],
					Title="Экзамен", Marks=new List<Mark>()},

				new Header()
				{ Id=7, Subject=AllSubjects[0], Teacher=AllTeachers[1], Group = AllGroups[0],
					Title="Зачёт", Marks=new List<Mark>()},
				new Header()
				{ Id=8, Subject=AllSubjects[1], Teacher=AllTeachers[1], Group=AllGroups[3],
					Title="Экзамен", Marks=new List<Mark>()},
				new Header()
				{ Id=9, Subject=AllSubjects[5], Teacher=AllTeachers[1], Group = AllGroups[0],
					Title="Контрольная работа", Marks=new List<Mark>()},
				new Header()
				{ Id=10, Subject=AllSubjects[1], Teacher=AllTeachers[1], Group = AllGroups[2],
					Title="5 декабря 2026 года", Marks=new List<Mark>()},

				new Header()
				{ Id=7, Subject=AllSubjects[5], Teacher=AllTeachers[2], Group = AllGroups[0],
					Title="02.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=8, Subject=AllSubjects[6], Teacher=AllTeachers[2], Group = AllGroups[1],
					Title="02.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=9, Subject=AllSubjects[7], Teacher=AllTeachers[2], Group = AllGroups[2],
					Title="02.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=10, Subject=AllSubjects[5], Teacher=AllTeachers[2], Group = AllGroups[0],
					Title="05.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=11, Subject=AllSubjects[6], Teacher=AllTeachers[2], Group = AllGroups[1],
					Title="09.09.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=12, Subject=AllSubjects[7], Teacher=AllTeachers[2], Group = AllGroups[2],
					Title="12.10.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=13, Subject=AllSubjects[5], Teacher=AllTeachers[2], Group = AllGroups[0],
					Title="5.11.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=14, Subject=AllSubjects[6], Teacher=AllTeachers[2], Group = AllGroups[1],
					Title="11.11.2026", Marks=new List<Mark>()},
				new Header()
				{ Id=15, Subject=AllSubjects[7], Teacher=AllTeachers[2], Group = AllGroups[2],
					Title="4.9.26", Marks=new List<Mark>()},
			};

			AllMarks = new List<Mark>()
			{
				new Mark() {Id = 0, Content="+", Header = AllHeaders[5], Student=AllStudents[9]},
				new Mark() {Id = 1, Content="+", Header = AllHeaders[4], Student=AllStudents[10]},
				new Mark() {Id = 2, Content="+", Header = AllHeaders[4], Student=AllStudents[11]},
				new Mark() {Id = 3, Content="+", Header = AllHeaders[0], Student=AllStudents[3]},
				new Mark() {Id = 4, Content="+", Header = AllHeaders[0], Student=AllStudents[4]},
				new Mark() {Id = 5, Content="+", Header = AllHeaders[0], Student=AllStudents[5]},
				new Mark() {Id = 6, Content="5", Header = AllHeaders[2], Student=AllStudents[3]},
				new Mark() {Id = 8, Content="2", Header = AllHeaders[2], Student=AllStudents[5]},
				new Mark() {Id = 9, Content="зачёт", Header = AllHeaders[3], Student=AllStudents[3]},
				new Mark() {Id = 10, Content="незачёт", Header = AllHeaders[3], Student=AllStudents[5]},
			};
		}
		internal Teacher? AuthenticateTeacher(Teacher teacher)
		{
			return AllTeachers.FirstOrDefault(x => x.AuthEquals(teacher));
		}
		internal List<Subject> GetSubjectsForTeacher(Teacher teacher)
		{
			return (from sub in AllTeaches where sub.Teacher.Equals(teacher) select sub.Subject).ToList();
		}
		internal List<Group> GetGroupsForSubject(Subject subject)
		{
			return (from stu in AllStudies where stu.Subject.Equals(subject) select stu.Group).ToList();
		}
		internal List<Student> GetStudentsForGroup(Group @group)
		{
			return (from student in AllStudents where student.Group.Equals(@group) select student).ToList();
		}
		internal List<Header> GetHeaders(Teacher teacher, Group @group, Subject subject)
		{
			return (from header in AllHeaders where (header.Group.Equals(@group) && header.Subject.Equals(subject) && header.Teacher.Equals(teacher)) select header).ToList();
		}
		internal List<Mark> GetMarks(Header header)
		{
			return (from mark in AllMarks where mark.Header.Equals(header) select mark).ToList();
		}
		internal void AddHeader(Header header)
		{
			AllHeaders.Add(new Header(header) { Id = AllHeaders.Max(x => x.Id) });
			foreach (var mark in header.Marks)
			{
				AllMarks.Add(new Mark() { Id = AllMarks.Max(x => x.Id), Header = mark.Header, Content = mark.Content, Student = mark.Student });
			}
		}
		internal void ChangeHeader(int headerToChangeId, Header header)
		{
			AllHeaders[headerToChangeId] = header;

			/* Этот код только для бд. В данной версии api он неприменим
			var headerToChange = AllHeaders.FirstOrDefault(x => x.Id == headerToChangeId);
			if (headerToChange != null)
			{
				AllHeaders.Remove(headerToChange);
				AllHeaders.Add(header);
			}
			*/
		}
		internal void RemoveHeader(int headerToRemoveId)
		{
			var headerToRemove = AllHeaders.FirstOrDefault(x => x.Id == headerToRemoveId);
			if (headerToRemove != null)
			{
				AllHeaders.Remove(headerToRemove);
				var marksToRemove = AllMarks.Where(x => x.Header.Id == headerToRemoveId).ToList();
				foreach (var markToRemove in marksToRemove)
				{
					AllMarks.Remove(markToRemove);
				}
			}
		}
	}
}
