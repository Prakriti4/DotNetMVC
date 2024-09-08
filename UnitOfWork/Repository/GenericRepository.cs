using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ViewModelTableFormation.Data;
using Microsoft.Extensions.Logging;
using ViewModelTableFormation.Models;

namespace ViewModelTableFormation.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : Student
    {
        protected readonly SchoolIdentityDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<GenericRepository<T>> _logger;
        private SchoolIdentityDbContext context;
        private ILogger logger;

        public GenericRepository(SchoolIdentityDbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        //public GenericRepository(SchoolIdentityDbContext context, ILogger logger)
        //{
        //    this.context = context;
        //    this.logger = logger;
        //}

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Entity not found for deletion.");
                return false;
            }

            _dbSet.Remove(entity);
            return true;
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync() // Implement this method
        {
            return await _context.Courses.ToListAsync();
        }
    }
}
