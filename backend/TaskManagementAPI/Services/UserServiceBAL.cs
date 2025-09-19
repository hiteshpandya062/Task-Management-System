using DataAccessLayer;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Services
{
    public class UserServiceBAL : IUserServiceBAL
    {
        private readonly IUserServiceDAL _userServiceDAL;
        public UserServiceBAL(IUserServiceDAL userServiceDAL)
        {
            _userServiceDAL = userServiceDAL;
        }

        public async Task<ApiResponse<User>> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ApiResponse<User>
                {
                    Errors = new List<string>() { "Email is null or empty." },
                    Message = " Email is null or empty.",
                    Result = null,
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            User userDetails = await _userServiceDAL.GetUserByEmail(email);
            if (userDetails == null)
            {
                return new ApiResponse<User>
                {
                    Errors = new List<string>() { "User Details not found for given email." },
                    Message = "User Details not found for given email.",
                    Result = null,
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
                return new ApiResponse<User>
                {
                    Errors = new List<string>() { },
                    Message = "User Details found.",
                    Result = userDetails,
                    StatusCode = StatusCodes.Status200OK
                };
        }
        public async Task<ApiListResponse<List<User>>> GetUsersForTaskAssignment()
        {

            List<User> userDetails = await _userServiceDAL.GetUsersForTaskAssignment();
            if (userDetails == null)
            {
                return new ApiListResponse<List<User>>
                {
                    Errors = new List<string>() { "User Details not found for given email." },
                    Message = "User Details not found for given email.",
                    Result = null,
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
                return new ApiListResponse<List<User>>
                {
                    Errors = new List<string>() { },
                    Message = "User Details found.",
                    Result = userDetails,
                    StatusCode = StatusCodes.Status200OK,
                    TotalCount = userDetails.Count
                };
        }
    }
}
