using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Data
{
	public class SchoolIdentityDbContext : IdentityDbContext
	{
		public SchoolIdentityDbContext(DbContextOptions<SchoolIdentityDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Course>()
				.HasMany(f => f.Students)
				.WithOne(c => c.Course)
				.HasForeignKey(c => c.CourseId);

			builder.Entity<Course>()
				.HasMany(f => f.Teachers)
				.WithOne(t => t.Course)
				.HasForeignKey(t => t.CourseId);

			builder.Entity<Faculty>()
				.HasMany(f => f.Courses)
				.WithOne(s => s.Faculty)
				.HasForeignKey(s => s.FacultyId);

			this.SeedRoles(builder);
			this.SeedUsers(builder);
			this.SeedUserRoles(builder);

			base.OnModelCreating(builder);
		}

		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Course> Courses { get; set; }

		private void SeedUsers(ModelBuilder builder)
		{
			User user = new User()
			{
				Id = "fab4fac1-c546-41de-aebc-a14da6895711",
				UserName = "admin@gmail.com",
				Email = "admin@gmail.com",
				NormalizedEmail = "ADMIN@GMAIL.COM",
				LockoutEnabled = false,
				EmailConfirmed = true
			};

			PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
			user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

			builder.Entity<User>().HasData(user);
		}


		private void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
				new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
				);
		}

		private void SeedUserRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string>() { UserId = "b74ddd14-6340-4840-95c2-db12554843e5", RoleId = "fab4fac1-c546-41de-aebc-a14da6895711" }
				);
		}
	}
}