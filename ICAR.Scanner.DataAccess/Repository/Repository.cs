using ICAR.Scanner.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ICAR.Scanner.DataAccess.Repository;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ICARDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ICARDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }



