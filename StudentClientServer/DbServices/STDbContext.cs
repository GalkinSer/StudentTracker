using Microsoft.EntityFrameworkCore;
using StudentTrackerLib.Models;

namespace StudentTrackerServer.DbServices
{
    public class STDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Studies> Studies { get; set; }
        public DbSet<Teaches> Teaches { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public STDbContext(DbContextOptions<STDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=st;Username=postgres;Password=password");
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
            modelBuilder.Entity<Studies>().HasIndex(st => new { subjectId = st.SubjectId, groupId = st.GroupId }).IsUnique();
            modelBuilder.Entity<Teaches>().HasIndex(tc => new { subjectId = tc.SubjectId, teacherId = tc.TeacherId }).IsUnique();
            modelBuilder.Entity<Mark>().HasIndex(m => m.Id).IsUnique();
            //отношения
            modelBuilder.Entity<Subject>().HasMany(x => x.Groups)
                .WithMany(x => x.Subjects)
                .UsingEntity(typeof(Studies));
            modelBuilder.Entity<Subject>().HasMany(x => x.Teachers)
                .WithMany(x => x.Subjects)
                .UsingEntity(typeof(Teaches));

            modelBuilder.Entity<Group>().HasMany(x => x.Students)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>().HasMany(x => x.Marks)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Group>().HasMany(x => x.Headers)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Subject>().HasMany(x => x.Headers)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Teacher>().HasMany(x => x.Headers)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Header>().HasMany(x => x.Marks)
                .WithOne(x => x.Header)
                .HasForeignKey(x => x.HeaderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
