using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.ITaskItemRepository
{
    public interface ITaskItemRepository
    {
        Task AddTaskItemAsync(TaskItemModel taskItem);
        Task<TaskItemModel?> GetTaskItemtByIdAsync(Guid id);
    }
}