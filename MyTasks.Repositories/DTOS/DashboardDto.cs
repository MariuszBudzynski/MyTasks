using MyTasks.Models.Models;

namespace MyTasks.Repositories.DTOS
{
    public record DashboardDto(
     Guid UserId,
     string Username,
     string FullName,
     UserType UserType,
     int ProjectCount,
     int TaskCount,
     int CompletedTasks,
     int PendingTasks,
     List<ProjectDto> Projects,
     List<TaskDto> Tasks);
}
