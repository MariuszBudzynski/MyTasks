namespace MyTasks.Repositories.DTOS
{
    public record TaskDto(
     Guid Id,
     string Title,
     string? Description,
     DateTime? DueDate,
     bool IsCompleted,
     string ProjectName,
     string? LastComment,
     DateTime? LastCommentAt);
}
