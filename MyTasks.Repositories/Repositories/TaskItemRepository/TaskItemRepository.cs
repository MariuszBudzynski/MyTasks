using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repositories;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;

namespace MyTasks.Repositories.Repositories.TaskItemRepository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IDbRepository<TaskItemModel> _taskItemRepository;
        private readonly ITaskItemOperationsRepository _taskItemOperationsRepository;
        public TaskItemRepository(IDbRepository<TaskItemModel> taskItemRepository,
            ITaskItemOperationsRepository taskItemOperationsRepository)
        {
            _taskItemRepository = taskItemRepository;
            _taskItemOperationsRepository = taskItemOperationsRepository;
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

        public async Task DeleteTaskItemtByIdAsync(Guid id)
        {
            await _taskItemOperationsRepository.DeleteTaskItemtWithCommentsByIdAsync(id);
        }
    }
}
