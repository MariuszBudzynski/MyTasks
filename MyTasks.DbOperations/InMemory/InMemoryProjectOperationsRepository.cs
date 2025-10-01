using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.InMemory
{
    public class InMemoryProjectOperationsRepository : IProjectOperationsRepository
    {
        private readonly InMemoryDbContext _context;

        public InMemoryProjectOperationsRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Task<ICollection<ProjectModel>> GetProjectsWithTasksAndCommentsAsync(string userName)
        {
            var userLogin = _context.Logins.FirstOrDefault(l => l.Username == userName);

            if (userLogin == null)
                throw new InvalidOperationException($"User '{userName}' does not exist.");

            var projects = _context.Projects
                .Where(p => p.OwnerId == userLogin.UserId)
                .ToList();

            foreach (var project in projects)
            {
                project.Owner = _context.Users.FirstOrDefault(u => u.Id == project.OwnerId);
                if (project.Owner != null)
                {
                    project.Owner.LoginModel = _context.Logins.FirstOrDefault(l => l.Id == project.Owner.LoginId);
                }

                project.Tasks = _context.Tasks.Where(t => t.ProjectId == project.Id).ToList();

                foreach (var task in project.Tasks)
                {
                    task.Comments = _context.Comments.Where(c => c.TaskItemId == task.Id).ToList();
                }
            }

            return Task.FromResult((ICollection<ProjectModel>)projects);
        }

        public Task<ProjectModel?> GetProjectWithAdditionalDataByIdAsync(Guid projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null) return Task.FromResult<ProjectModel?>(null);

            project.Tasks = _context.Tasks.Where(t => t.ProjectId == project.Id).ToList();

            foreach (var task in project.Tasks)
                task.Comments = _context.Comments.Where(c => c.TaskItemId == task.Id).ToList();

            return Task.FromResult(project);
        }

        public Task DeleteProjectWithTasksAndCommentsByIdAsync(Guid projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null) return Task.CompletedTask;

            foreach (var task in project.Tasks.ToList())
            {
                _context.Comments.RemoveAll(c => c.TaskItemId == task.Id);
                _context.Tasks.Remove(task);
            }

            _context.Projects.Remove(project);

            return Task.CompletedTask;
        }
    }
}