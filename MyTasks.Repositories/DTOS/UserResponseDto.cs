using MyTasks.Models.Models;

namespace MyTasks.Repositories.DTOS
{
    public record UserResponseDto(
       Guid Id,
       string FullName,
       string Username,
       UserType Type,
       bool IsDeleted
   );
}
