namespace MyTasks.Repositories.DTOS
{
    public record ProjectResponseDto(
         Guid Id,
         string Name,
         string? Description,
         Guid OwnerId
     );
}
