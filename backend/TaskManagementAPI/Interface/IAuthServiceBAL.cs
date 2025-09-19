using DataAccessLayer.ViewModels;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Interface
{
    public interface IAuthServiceBAL
    {
        Task<ApiResponse<bool>> Register(UsersRegisterVM usersRegister);
        Task<ApiResponse<string>> Login(string email, string password);
    }
}
