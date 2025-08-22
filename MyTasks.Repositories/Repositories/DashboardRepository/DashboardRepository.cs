using MyTasks.DbOperations.Interface;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IDashboardRepository;

namespace MyTasks.Repositories.Repositories.DashboardRepository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IProjectRepository _projectRepository;
        public DashboardRepository(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<DashboardDto?> GetProjectsData(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User Name can't be empty");
            }

            var projects = await _projectRepository.GetProjectsWithTasksAndComments(userName);

            if (!projects.Any())
            {
                throw new ArgumentException("Projects not found");
            }

            return new DashboardDto(
                UserId: projects.FirstOrDefault()?.Owner?.Id ?? Guid.Empty,
                Username: projects.FirstOrDefault()?.Owner?.LoginModel?.Username ?? "",
                FullName: projects.FirstOrDefault()?.Owner?.FullName ?? "",
                UserType: projects.FirstOrDefault()?.Owner?.LoginModel?.Type ?? Models.Models.UserType.Regular,
                ProjectCount: projects.Count,
                TaskCount: projects.SelectMany(p => p.Tasks).Count(),
                CompletedTasks: projects.SelectMany(p => p.Tasks).Count(t => t.IsCompleted),
                PendingTasks: projects.SelectMany(p => p.Tasks).Count(t => t.IsCompleted),
                Projects: projects.Select(p => new ProjectDto(
                    Id: p.Id,
                    Name: p.Name,
                    Description: p.Description,
                    TaskCount: p.Tasks.Count,
                    CompletedTasks: p.Tasks.Count(t => t.IsCompleted)
                )).ToList(),
                Tasks: projects.SelectMany(p => p.Tasks).Select(t => new TaskDto(
                    Id: t.Id,
                    Title: t.Title,
                    Description: t.Description,
                    DueDate: t.DueDate,
                    IsCompleted: t.IsCompleted,
                    ProjectName: t.Project?.Name ?? "",
                    LastComment: t.Comments
                                .OrderByDescending(c => c.CreatedAt)
                                .FirstOrDefault()?.Content,
                    LastCommentAt: t.Comments
                                   .OrderByDescending(c => c.CreatedAt)
                                   .FirstOrDefault()?.CreatedAt
                )).ToList()
            );
        }
    }
}
