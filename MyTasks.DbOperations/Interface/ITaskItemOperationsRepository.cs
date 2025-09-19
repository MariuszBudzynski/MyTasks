using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface ITaskItemOperationsRepository
    {
        Task<TaskItemModel?> GetTaskItemtWithAdditionalDataByIdAsync(Guid taskItemtId);
        Task DeleteTaskItemtWithCommentsByIdAsync(Guid taskItemtId);
    }
}