using ViewModelTableFormation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModelTableFormation.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
