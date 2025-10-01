using MyTasks.DbOperations.Interface;
using MyTasks.Models.Interfaces;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.InMemory
{
    public class InMemoryRepository<TEntity> : IDbRepository<TEntity> where TEntity : BaseModel
    {
        private readonly InMemoryDbContext _context;
        private readonly List<TEntity> _dbSet;

        public InMemoryRepository(InMemoryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public Task UpdateEntityAsync(TEntity entity)
        {
            var existing = _dbSet.FirstOrDefault(e => e.Id == entity.Id);
            if (existing != null)
            {
                var index = _dbSet.IndexOf(existing);
                _dbSet[index] = entity;
            }
            return Task.CompletedTask;
        }

        public Task<ICollection<TEntity>> GetAll()
        {
            return Task.FromResult((ICollection<TEntity>)_dbSet);
        }

        public Task AddEntityAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = _dbSet.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entity);
        }

        public Task<T?> GetByUserNameAsync<T>(string username)
            where T : class, TEntity, IUsername, IType
        {
            var entity = _dbSet.OfType<T>().FirstOrDefault(e => e.Username == username);
            return Task.FromResult(entity);
        }

        public Task UpdateByIdAsync(Guid id)
        {
            var entityToUpdate = _dbSet.FirstOrDefault(e => e.Id == id);
            if (entityToUpdate != null)
            {
                var index = _dbSet.IndexOf(entityToUpdate);
                _dbSet[index] = entityToUpdate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            var entityToRemove = _dbSet.FirstOrDefault(e => e.Id == id);
            if (entityToRemove != null)
            {
                _dbSet.Remove(entityToRemove);
            }
            return Task.CompletedTask;
        }
    }
}