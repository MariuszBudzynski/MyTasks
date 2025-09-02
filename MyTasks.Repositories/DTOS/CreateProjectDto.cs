using System.Text.Json.Serialization;

namespace MyTasks.Repositories.DTOS
{
    public record CreateProjectDto(
        string Name,
        string? Description)
    {
        [JsonIgnore]
        public Guid OwnerId { get; init; }
    }
}
