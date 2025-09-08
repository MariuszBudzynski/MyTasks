using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.ITaskCommentRepository
{
    public interface ITaskCommentRepository
    {
        Task AddTaskComment(TaskCommentModel taskComment);
        Task<TaskCommentModel?> GetByIdAsync(Guid id);
    }
}