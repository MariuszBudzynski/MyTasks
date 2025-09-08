namespace MyTasks.Repositories.DTOS
{
    public record TaskCommentResponseDto(
        Guid Id,
        string Content,
        DateTime CreatedAt
        );
}
