using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;
using ViewModelTableFormation.Repository;

namespace ViewModelTableFormation.UnitOfWork
{
    public class StudentUOW : IUnitOfWork
    {
        private readonly SchoolIdentityDbContext _context;
        private IStudentRepository _students;

        public StudentUOW(SchoolIdentityDbContext context, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _context = context;
            _students = studentRepository;
            _courses= courseRepository;
        }

        public IStudentRepository Students
        {
            get { return _students; }
        }

        public ICourseRepository Courses  // Implement the course repository getter
        {
            get { return _courses; }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
