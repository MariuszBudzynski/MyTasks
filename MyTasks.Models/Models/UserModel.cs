namespace MyTasks.Models.Models
{
    public class UserModel : BaseModel
    {
        public override Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Guid LoginId { get; set; }
        public LoginModel? LoginModel { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
        public ICollection<TaskItemModel> AssignedTasks { get; set; } = new List<TaskItemModel>();
    }
}
