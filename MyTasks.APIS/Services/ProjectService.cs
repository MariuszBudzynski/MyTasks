using MyTasks.API.Services.Interfaces;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjecRepository _projectRepository;
        public ProjectService(IProjecRepository projectRepository, IUserOperationsRepository userRepository)
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

        public async Task<ProjectModel?> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetById(id);
        }
    }
}
