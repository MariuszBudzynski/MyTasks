using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Interfaces;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : BaseModel
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public DbRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid Id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<T?> GetByUserNameAsync<T>(string username)
            where T : class, TEntity, IUsername, IType
        {
            return await _dbSet.OfType<T>().FirstOrDefaultAsync(e => e.Username == username);
        }

        public async Task UpdateByIdAsync(Guid Id)
        {
            var entityToUpdate = await _dbSet.FirstOrDefaultAsync(e => e.Id == Id);
            if (entityToUpdate == null) return;

            _dbSet.Update(entityToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid Id)
        {
            var entityToUpdate = await _dbSet.FirstOrDefaultAsync(e => e.Id == Id);
            if (entityToUpdate == null) return;

            _dbSet.Remove(entityToUpdate);
            await _context.SaveChangesAsync();
        }
    }
}
