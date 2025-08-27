namespace MyTasks.Models.Models
{
    public class ProjectModel : BaseModel
    {
        public override Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? OwnerId { get; set; }
        public UserModel? Owner { get; set; }

        public ICollection<TaskItemModel> Tasks { get; set; } = new List<TaskItemModel>();
    }
}
