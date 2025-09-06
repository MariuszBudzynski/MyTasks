using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Interface;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IDashboardRepository;

namespace MyTasks.Repositories.Repositories.DashboardRepository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IProjectOperationsRepository _projectRepository;
        public DashboardRepository(IProjectOperationsRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<DashboardDto?> GetProjectsDataAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User Name can't be empty");
            }

            var projectsQuery = await _projectRepository.GetProjectsWithTasksAndCommentsAsync(userName);
            var projects = await projectsQuery.ToListAsync();

            if (!projects.Any())
            {
                return new DashboardDto(
                   UserId: Guid.Empty,
                   Username: userName,
                   FullName: string.Empty,
                   UserType: Models.Models.UserType.Regular,
                   ProjectCount: 0,
                   TaskCount: 0,
                   CompletedTasks: 0,
                   PendingTasks: 0,
                   Projects: new List<ProjectDto>(),
                   Tasks: new List<TaskDto>()
               );
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
