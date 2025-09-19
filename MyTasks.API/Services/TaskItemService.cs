using MyTasks.API.Services.Interfaces;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;

namespace MyTasks.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemModel?> GetTaskItemById(Guid id)
        {
            return await _taskItemRepository.GetTaskItemtByIdAsync(id);
        }

        public async Task UpdateTaskItemtAsync(UpdateTaskItemDto taskItemToUpdate, TaskItemModel taskItem, Guid taskItemId)
        {
            taskItem.Title = taskItemToUpdate.Title;
            taskItem.Description = taskItemToUpdate.Description;
            taskItem.IsCompleted = taskItemToUpdate.IsCompleted;
            taskItem.DueDate = taskItemToUpdate.DueDate ?? DateTime.MinValue;
            await _taskItemRepository.UpdateTaskItemAsync(taskItem);
        }

        public async Task<TaskItemResponseDto?> GetByIdAsync(Guid id)
        {
            var taskItem = await _taskItemRepository.GetTaskItemtByIdAsync(id);
            if (taskItem == null) return null;

            return new TaskItemResponseDto(
                taskItem.Id,
                taskItem.Title,
                taskItem.Description,
                taskItem.DueDate ?? DateTime.MinValue,
                taskItem.IsCompleted,
                taskItem.ProjectId,
                taskItem.AssignedUserId
            );
        }

        public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto data, Guid ownerId)
        {
            var taskItemId = Guid.NewGuid();

            var taskItem = new TaskItemModel
            {
                Id = taskItemId,
                Title = data.Title,
                Description = data.Description,
                DueDate = data.DueDate,
                IsCompleted = data.IsCompleted,
                ProjectId = data.ProjectId,
                AssignedUserId = ownerId
            };

            await _taskItemRepository.AddTaskItemAsync(taskItem);

            return new TaskItemResponseDto(
                taskItemId,
                taskItem.Title,
                taskItem.Description,
                taskItem.DueDate,
                taskItem.IsCompleted,
                taskItem.ProjectId,
                taskItem.AssignedUserId
            );
        }

        public async Task DeleteTaskItemtWithDataByIdAsync(Guid id)
        {
            await _taskItemRepository.DeleteTaskItemtByIdAsync(id);
        }
    }
}
