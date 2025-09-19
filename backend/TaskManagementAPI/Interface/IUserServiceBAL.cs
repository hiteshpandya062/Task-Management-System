using DataAccessLayer;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Interface
{
    public interface IUserServiceBAL
    {
        Task<ApiResponse<User>> GetUserByEmail(string email);
        Task<ApiListResponse<List<User>>> GetUsersForTaskAssignment();
    }
}
