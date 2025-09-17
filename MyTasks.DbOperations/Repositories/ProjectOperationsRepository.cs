using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class ProjectOperationsRepository : IProjectOperationsRepository
    {
        private readonly AppDbContext _context;
        public ProjectOperationsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<ProjectModel>> GetProjectsWithTasksAndCommentsAsync(string userName)
        {
            var userId = (await _context.Login.FirstOrDefaultAsync(l => l.Username == userName))?.UserId;

            if (userId == null)
            {
                throw new InvalidOperationException($"User '{userName}' does not exist.");
            }

            return _context.Project
                                 .Where(p => p.OwnerId == userId)
                                 .Include(p => p.Owner)
                                    .ThenInclude(o => o.LoginModel)
                                 .Include(p => p.Tasks)
                                    .ThenInclude(t => t.Comments);
        }

        public async Task<ProjectModel?> GetProjectWithAdditionalDataByIdAsync(Guid projectId)
        {
            return await _context.Project
                                 .Where(p => p.Id == projectId)
                                .Include(p => p.Tasks)
                                    .ThenInclude( t => t.Comments)
                                .FirstOrDefaultAsync();
        }

        public async Task DeleteProjectWithTasksAndCommentsByIdAsync(Guid projectId)
        {
            var project = await GetProjectWithAdditionalDataByIdAsync(projectId);

            _context.Project.Remove(project);
            _context.RemoveRange(project.Tasks);
            _context.RemoveRange(project.Tasks.SelectMany(t => t.Comments));
            await _context.SaveChangesAsync();   
        }
    }
}