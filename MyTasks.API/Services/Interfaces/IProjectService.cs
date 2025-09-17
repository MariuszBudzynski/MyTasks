using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto data, Guid ownerId);
        Task<ProjectModel?> GetProjectByIdAsync(Guid id);
        Task UpdateProjectAsync(UpdateProjectDto projectToUpdate, ProjectModel project, Guid projectId);
        Task DeleteProjectWithDataByIdAsync(Guid id);
    }
}