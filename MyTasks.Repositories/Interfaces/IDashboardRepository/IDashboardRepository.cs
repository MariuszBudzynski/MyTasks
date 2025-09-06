using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IDashboardRepository
{
    public interface IDashboardRepository
    {
        Task<DashboardDto?> GetProjectsDataAsync(string userName);
    }
}