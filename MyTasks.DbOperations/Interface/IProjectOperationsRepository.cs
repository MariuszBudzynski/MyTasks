using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IProjectOperationsRepository
    {
        Task<IQueryable<ProjectModel>> GetProjectsWithTasksAndCommentsAsync(string userName);
    }
}