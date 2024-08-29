using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
          .HasMany(f => f.Students)
          .WithOne(c => c.Course)
          .HasForeignKey(c => c.CourseId);

        modelBuilder.Entity<Course>()
            .HasMany(f => f.Teachers)
            .WithOne(t => t.Course)
            .HasForeignKey(t => t.CourseId);

        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Courses)
            .WithOne(s => s.Faculty)
            .HasForeignKey(s => s.FacultyId);

        base.OnModelCreating(modelBuilder);

    }

}