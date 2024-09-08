
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Repository
{
    public interface IStudentRepository : IRepository<Student>  
    {
    }  
}
