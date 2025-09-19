using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        public DateTime createdAt { get; set; }
        public ICollection<TaskDetail> Tasks { get; set; } = new List<TaskDetail>();
    }
}
