using ViewModelTableFormation.Data;
using ViewModelTableFormation.Repository;

namespace ViewModelTableFormation.UnitOfWork
{

    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        Task<int> SaveChangesAsync();
        Task CompleteAsync();
    }
}