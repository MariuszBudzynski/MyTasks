namespace MyTasks.Models.Models
{
    public class TaskItemModel : BaseModel
    {
        public override Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Guid ProjectId { get; set; }
        public ProjectModel? Project { get; set; }

        public Guid? AssignedUserId { get; set; }
        public UserModel? AssignedUser { get; set; }

        public ICollection<TaskCommentModel> Comments { get; set; } = new List<TaskCommentModel>();
    }
}
