using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.IProjecRepository
{
    public interface IProjecRepository
    {
        Task AddProject(ProjectModel project);
        Task<ProjectModel?> GetById(Guid id);
        Task UpdateProject(ProjectModel project);
    }
}