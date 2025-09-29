using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.IProjecRepository
{
    public interface IProjecRepository
    {
        Task AddProject(ProjectModel project);
        Task<ProjectModel?> GetByIdAsync(Guid id);
        Task UpdateProject(ProjectModel project);
        Task DeleteProjectByIdAsync(Guid id);
        Task<ICollection<ProjectModel>> GetAllProjects();
    }
}