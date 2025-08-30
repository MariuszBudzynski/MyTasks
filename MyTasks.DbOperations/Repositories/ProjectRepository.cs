using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<ProjectModel>> GetProjectsWithTasksAndComments(string userName)
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
    }
}