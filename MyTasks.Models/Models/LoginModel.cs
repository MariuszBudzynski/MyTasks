using MyTasks.Models.Interfaces;

namespace MyTasks.Models.Models
{
    public class LoginModel : BaseModel, IUsername
    {
        public override Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserType Type { get; set; }

        public Guid UserId { get; set; }
        public UserModel? User { get; set; }
    }
    public enum UserType
    {
        Admin = 0,
        Regular = 1
    }
}
