namespace MyTasks.Repositories.DTOS
{
    public record UpdateTaskItemDto(
        string Title,
        string? Description,
        DateTime? DueDate,
        bool IsCompleted);
}
