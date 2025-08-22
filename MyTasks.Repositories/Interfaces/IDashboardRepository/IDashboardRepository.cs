using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IDashboardRepository
{
    public interface IDashboardRepository
    {
        Task<DashboardDto?> GetProjectsData(string userName);
    }
}