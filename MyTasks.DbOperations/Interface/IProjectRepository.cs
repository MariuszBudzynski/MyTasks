using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IProjectRepository
    {
        Task<IQueryable<ProjectModel>> GetProjectsWithTasksAndComments(string userName);
    }
}