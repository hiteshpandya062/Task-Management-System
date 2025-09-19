using DataAccessLayer;

namespace TaskManagementAPI.Interface
{
    public interface IUserServiceDAL
    {
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUsersForTaskAssignment();
    }
}
