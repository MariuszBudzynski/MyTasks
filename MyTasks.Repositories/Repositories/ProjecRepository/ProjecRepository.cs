using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.Repositories.Repositories.ProjecRepository
{
    public class ProjecRepository : IProjecRepository
    {
        private readonly IDbRepository<ProjectModel> _projectRepository;
        private readonly IProjectOperationsRepository _projectOperationsRepository;
        public ProjecRepository(IDbRepository<ProjectModel> projectRepository, IProjectOperationsRepository projectOperationsRepository)
        {
            _projectRepository = projectRepository;
            _projectOperationsRepository = projectOperationsRepository;
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

        public async Task DeleteProjectByIdAsync(Guid id)
        {
            await _projectOperationsRepository.DeleteProjectWithTasksAndCommentsByIdAsync(id);
        }

        public async Task<ICollection<ProjectModel>> GetAllProjects()
        {
            return await _projectRepository.GetAll().ToListAsync();
        }
    }
}
