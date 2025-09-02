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
        private readonly IUserOperationsRepository _userRepository;

        public ProjectService(IProjecRepository projectRepository, IUserOperationsRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto data, Guid ownerId)
        {
            var user = await _userRepository.GetUserByIdAsync(ownerId);
            if (user == null)
                throw new InvalidOperationException("User does not exist.");

            var project = new ProjectModel
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Description = data.Description,
                OwnerId = ownerId,
                Owner = user
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
