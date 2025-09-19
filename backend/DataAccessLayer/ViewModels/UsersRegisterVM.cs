
namespace DataAccessLayer.ViewModels
{
    public class UsersRegisterVM
    {
        public string Userame { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; } = Roles.User;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
