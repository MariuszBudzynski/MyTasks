namespace MyTasks.Repositories.DTOS
{
    public record TaskItemResponseDto(
         Guid Id,
         string Title,
         string? Description,
         DateTime? DueDate,
         bool IsCompleted,
         Guid? ProjectId,
         Guid? AssignedUserId
     );
}
