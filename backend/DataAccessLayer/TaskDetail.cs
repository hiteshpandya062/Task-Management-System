using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class TaskDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public Priority priority { get; set; }
        public int assigneeId { get; set; }
        public int creatorId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }

        public User Assignee { get; set; }
        public User Creator { get; set; }
    }
}