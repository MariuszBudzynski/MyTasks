using MyTasks.Models.Interfaces;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IDbRepository<TEntity> where TEntity : BaseModel
    {
        Task DeleteByIdAsync(Guid Id);
        Task AddEntityAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(Guid Id);
        Task UpdateByIdAsync(Guid Id);
        Task<T?> GetByUserNameAsync<T>(string username)
            where T : class, TEntity, IUsername, IType;
    }
}