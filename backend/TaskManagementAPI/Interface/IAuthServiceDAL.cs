using DataAccessLayer;

namespace TaskManagementAPI.Interface
{
    public interface IAuthServiceDAL
    {
        Task<bool> Register(User userDetails);
     }
}
