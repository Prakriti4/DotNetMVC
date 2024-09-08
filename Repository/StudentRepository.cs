using Microsoft.Extensions.Logging;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ViewModelTableFormation.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolIdentityDbContext context, ILogger<StudentRepository> logger)
            : base(context, logger)
        { }

        // Override method to get all students, including related entities like Courses
        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            var result = await _dbSet/*Include(s => s.Course)*/.ToListAsync();

            return result; //yo call vairako xa jasma aarkai code xa
        }

        // Get student by ID including related entities like Course
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _dbSet.Include(s => s.Course)
                               .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Add a new student to the database
        public override async Task<bool> AddAsync(Student student)
        {
            await _dbSet.AddAsync(student);
            return true;
        }

        // Update an existing student in the database
        public override async Task<bool> UpdateAsync(Student student)
        {
            _dbSet.Update(student);
            return true;
        }

        // Delete a student by ID from the database
        public override async Task<bool> DeleteAsync(int id)
        {
            var student = await GetStudentByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning($"Student with ID {id} not found for deletion.");
                return false;
            }
            // Save changes to the database
            await _context.SaveChangesAsync();

            _dbSet.Remove(student);
            return true;
        }

        // Find students based on a condition
        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Optional: Get all courses (if this is required directly in the repository)
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
