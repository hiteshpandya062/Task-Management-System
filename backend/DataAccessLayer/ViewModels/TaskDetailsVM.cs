namespace DataAccessLayer.ViewModels
{
    public class AddUpdateTaskDetailsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public int AssigneeId { get; set; }
        public int CreatorId { get; set; }
    }

    public class TaskDetailsVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string AssigneeName { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}