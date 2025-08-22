using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IProjectRepository
    {
        Task<ICollection<ProjectModel>> GetProjectsWithTasksAndComments(string userName);
    }
}