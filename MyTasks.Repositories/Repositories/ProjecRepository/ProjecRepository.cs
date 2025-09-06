using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.Repositories.Repositories.ProjecRepository
{
    public class ProjecRepository : IProjecRepository
    {
        private readonly IDbRepository<ProjectModel> _projectRepository;
        public ProjecRepository(IDbRepository<ProjectModel> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task AddProject(ProjectModel project)
        {
            await _projectRepository.AddEntityAsync(project);
        }

        public async Task UpdateProject(ProjectModel project)
        {
            await _projectRepository.UpdateEntityAsync(project);
        }

        public async Task<ProjectModel?> GetByIdAsync(Guid id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }
    }
}
