using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.ITaskCommentRepository;

namespace MyTasks.Repositories.Repositories.TaskCommentRepository
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly IDbRepository<TaskCommentModel> _taskCommentRepository;

        public TaskCommentRepository(IDbRepository<TaskCommentModel> taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        public async Task AddTaskComment(TaskCommentModel taskComment)
        {
            await _taskCommentRepository.AddEntityAsync(taskComment);
        }

        public async Task<TaskCommentModel?> GetByIdAsync(Guid id)
        {
            return await _taskCommentRepository.GetByIdAsync(id);
        }
    }
}
