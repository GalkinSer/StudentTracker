using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;
using StudentTrackerLib.Models.Operational;

namespace StudentClientServer.DbServices
{
	public class STDbContext : DbContext
	{
		private DbSet<Teacher> Teachers { get; set; }
		private DbSet<Student> Students { get; set; }
		private DbSet<Group> Groups { get; set; }
		private DbSet<Subject> Subjects { get; set; }
		private DbSet<Header> Headers { get; set; }
		private DbSet<Studies> Studies { get; set; }
		private DbSet<Teaches> Teaches { get; set; }
		private DbSet<Mark> Marks { get; set; }

		public STDbContext(DbContextOptions<STDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite("DataSource=StudentTracker.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//индексы
			modelBuilder.Entity<Teacher>().HasIndex(t => t.Id).IsUnique();
			modelBuilder.Entity<Student>().HasIndex(s => s.Id).IsUnique();
			modelBuilder.Entity<Group>().HasIndex(g => g.Id).IsUnique();
			modelBuilder.Entity<Subject>().HasIndex(sb => sb.Id).IsUnique();
			modelBuilder.Entity<Header>().HasIndex(h => h.Id).IsUnique();
			modelBuilder.Entity<Studies>().HasIndex(st => new { subjectId = st.Subject.Id, groupId = st.Group.Id }).IsUnique();
			modelBuilder.Entity<Teaches>().HasIndex(tc => new { subjectId = tc.Subject.Id, teacherId = tc.Teacher.Id }).IsUnique();
			modelBuilder.Entity<Mark>().HasIndex(m => m.Id).IsUnique();

			modelBuilder.Entity<Studies>().HasMany("Subject").WithMany("Group");
			modelBuilder.Entity<Teaches>().HasMany("Subject").WithMany("Teacher");
			modelBuilder.Entity<Header>().HasOne(h => h.)
		}
	}
}
