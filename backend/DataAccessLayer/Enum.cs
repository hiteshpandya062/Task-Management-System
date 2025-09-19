using System.ComponentModel;

namespace DataAccessLayer
{
    public enum Roles
    {
        User = 0,
        Admin = 1
    }
    public enum Status
    {
        [Description("To Do")]
        ToDo = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Done")]
        Done = 3,
    }
    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
}
