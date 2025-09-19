using MyTasks.API.Services.Interfaces;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjecRepository _projectRepository;
        public ProjectService(IProjecRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto data, Guid ownerId)
        {
            var project = new ProjectModel
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Description = data.Description,
                OwnerId = ownerId,
            };

            await _projectRepository.AddProject(project);

            return new ProjectResponseDto(
                project.Id,
                project.Name,
                project.Description,
                project.OwnerId ?? Guid.Empty
            );
        }

        public async Task UpdateProjectAsync(UpdateProjectDto projectToUpdate, ProjectModel project, Guid projectId)
        {
           project.Name = projectToUpdate.Name;
           project.Description = projectToUpdate.Description;
           await _projectRepository.UpdateProject(project);
        }

        public async Task<ProjectModel?> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task DeleteProjectWithDataByIdAsync(Guid id)
        {
            await _projectRepository.DeleteProjectByIdAsync(id);
        }
    }
}
