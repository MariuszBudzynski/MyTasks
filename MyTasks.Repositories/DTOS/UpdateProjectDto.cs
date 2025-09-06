using System.Text.Json.Serialization;

namespace MyTasks.Repositories.DTOS
{
    public record UpdateProjectDto(
        string Name,
        string? Description)
    {
        [JsonIgnore]
        public Guid OwnerId { get; init; }
    }
}
