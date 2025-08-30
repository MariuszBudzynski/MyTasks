using MyTasks.Models.Interfaces;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IDbRepository<TEntity> where TEntity : BaseModel
    {
        Task DeleteById(Guid Id);
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetById(Guid Id);
        Task UpdateById(Guid Id);
        Task<T?> GetByUserName<T>(string username)
            where T : class, TEntity, IUsername, IType;
    }
}