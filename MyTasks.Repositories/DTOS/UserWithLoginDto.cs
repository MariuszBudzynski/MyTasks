using MyTasks.Models.Models;
using System.Text.Json.Serialization;

namespace MyTasks.Repositories.DTOS
{
    public record UserWithLoginDto
    {
        [JsonIgnore]
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;

        // Login
        [JsonIgnore]
        public Guid LoginId { get; init; }

        // Login data
        public string Username { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;
        public UserType Type { get; init; }
        [JsonIgnore]
        public bool FakeUser { get; init; } = false;

        // User
        [JsonIgnore]
        public Guid UserId { get; init; }
    }
}
