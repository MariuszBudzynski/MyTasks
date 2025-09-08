namespace MyTasks.Repositories.DTOS
{
    public record CreateTaskCommentDto(
        string Content,
        DateTime CreatedAt,
        Guid? TaskItemId
        );
}
