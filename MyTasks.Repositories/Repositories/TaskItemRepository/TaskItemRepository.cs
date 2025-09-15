using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;

namespace MyTasks.Repositories.Repositories.TaskItemRepository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IDbRepository<TaskItemModel> _taskItemRepository;
        public TaskItemRepository(IDbRepository<TaskItemModel> taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task UpdateTaskItemAsync(TaskItemModel taskItem)
        {
            await _taskItemRepository.UpdateEntityAsync(taskItem);
        }

        public async Task AddTaskItemAsync(TaskItemModel taskItem)
        {
            await _taskItemRepository.AddEntityAsync(taskItem);
        }

        public async Task<TaskItemModel?> GetTaskItemtByIdAsync(Guid id)
        {
            return await _taskItemRepository.GetByIdAsync(id);
        }
    }
}
