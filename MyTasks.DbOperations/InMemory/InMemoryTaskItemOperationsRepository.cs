using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.InMemory
{
    public class InMemoryTaskItemOperationsRepository : ITaskItemOperationsRepository
    {
        private readonly InMemoryDbContext _context;

        public InMemoryTaskItemOperationsRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItemModel?> GetTaskItemtWithAdditionalDataByIdAsync(Guid taskItemtId)
        {
            var taskItem = _context.Tasks.FirstOrDefault(t => t.Id == taskItemtId);
            if (taskItem == null)
                return await Task.FromResult<TaskItemModel?>(null);

            taskItem.Comments = _context.Comments
                .Where(c => c.TaskItemId == taskItem.Id)
                .ToList();

            return await Task.FromResult(taskItem);
        }

        public async Task DeleteTaskItemtWithCommentsByIdAsync(Guid taskItemtId)
        {
            var taskItem = await GetTaskItemtWithAdditionalDataByIdAsync(taskItemtId);
            if (taskItem == null) return;

            _context.Comments.RemoveAll(c => c.TaskItemId == taskItem.Id);

            _context.Tasks.Remove(taskItem);

            await Task.CompletedTask;
        }
    }
}