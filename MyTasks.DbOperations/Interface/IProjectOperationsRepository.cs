using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IProjectOperationsRepository
    {
        Task<ICollection<ProjectModel>> GetProjectsWithTasksAndCommentsAsync(string userName);
        Task<ProjectModel?> GetProjectWithAdditionalDataByIdAsync(Guid projectId);
        Task DeleteProjectWithTasksAndCommentsByIdAsync(Guid projectId);
    }
}