using MyTasks.API.Services.Interfaces;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ITaskCommentRepository;

namespace MyTasks.API.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        public async Task<TaskCommentModel?> GetTaskCommentByIdAsync(Guid id)
        {
            return await _taskCommentRepository.GetByIdAsync(id);
        }

        public async Task<TaskCommentResponseDto> CreateTaskCommentAsync(CreateTaskCommentDto data, Guid ownerId)
        {
            var taskComment = new TaskCommentModel
            {
                Id = Guid.NewGuid(),
                Content = data.Content,
                CreatedAt = data.CreatedAt,
                TaskItemId = data.TaskItemId,
                AuthorId = ownerId,
            };

            await _taskCommentRepository.AddTaskComment(taskComment);

            return new TaskCommentResponseDto(
                taskComment.Id,
                taskComment.Content,
                taskComment.CreatedAt
                );
        }
    }
}
