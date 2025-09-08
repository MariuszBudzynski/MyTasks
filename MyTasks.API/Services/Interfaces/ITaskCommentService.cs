using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API.Services.Interfaces
{
    public interface ITaskCommentService
    {
        Task<TaskCommentResponseDto> CreateTaskCommentAsync(CreateTaskCommentDto data, Guid ownerId);
        Task<TaskCommentModel?> GetTaskCommentByIdAsync(Guid id);
    }
}