using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto data, Guid ownerId);
        Task<TaskItemResponseDto?> GetByIdAsync(Guid id);
        Task<TaskItemModel?> GetTaskItemById(Guid id);
        Task UpdateTaskItemtAsync(UpdateTaskItemDto taskItemToUpdate, TaskItemModel taskItem, Guid taskItemId);
    }
}