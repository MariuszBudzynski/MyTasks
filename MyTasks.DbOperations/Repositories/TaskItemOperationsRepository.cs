using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class TaskItemOperationsRepository : ITaskItemOperationsRepository
    {
        private readonly AppDbContext _context;
        public TaskItemOperationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItemModel?> GetTaskItemtWithAdditionalDataByIdAsync(Guid taskItemtId)
        {
            return await _context.TaskItem
                                 .Where(p => p.Id == taskItemtId)
                                .Include(p => p.Comments)
                                .FirstOrDefaultAsync();
        }

        public async Task DeleteTaskItemtWithCommentsByIdAsync(Guid taskItemtId)
        {
            var taskItem = await GetTaskItemtWithAdditionalDataByIdAsync(taskItemtId);

            _context.TaskItem.Remove(taskItem);
            _context.RemoveRange(taskItem.Comments);
            await _context.SaveChangesAsync();
        }
    }
}
