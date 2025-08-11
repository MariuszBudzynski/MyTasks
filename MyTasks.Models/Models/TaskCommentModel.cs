namespace MyTasks.Models.Models
{
    public class TaskCommentModel : BaseModel
    {
        public override Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid TaskItemId { get; set; }
        public TaskItemModel? TaskItem { get; set; }

        public Guid AuthorId { get; set; }
        public UserModel? Author { get; set; }
    }
}
