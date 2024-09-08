using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;
using ViewModelTableFormation.UnitOfWork;

namespace ViewModelTableFormation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SchoolIdentityDbContext _context;

        public StudentService(IUnitOfWork unitOfWork, SchoolIdentityDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _unitOfWork.Students.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
