namespace MyTasks.Repositories.DTOS
{
    public record CreateTaskItemDto(
         string Title,
         string? Description,
         DateTime? DueDate,
         bool IsCompleted,
         Guid? ProjectId
     );
}
