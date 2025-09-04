using MyTasks.Repositories.DTOS;

namespace MyTasks.API.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto data, Guid ownerId);
        Task<TaskItemResponseDto?> GetByIdAsync(Guid id);
    }
}