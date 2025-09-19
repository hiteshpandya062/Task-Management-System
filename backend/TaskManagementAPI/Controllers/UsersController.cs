using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceBAL _userServiceBAL;
        public UsersController(IUserServiceBAL userServiceBAL)
        {
            _userServiceBAL = userServiceBAL;
        }

        [HttpGet]
        public async Task<ActionResult<ApiListResponse<List<User>>>> GetUsers()
        {
            var apiResponse = await _userServiceBAL.GetUsersForTaskAssignment();
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(apiResponse),
                StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                StatusCodes.Status404NotFound => NotFound(apiResponse),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                _ => BadRequest(apiResponse),
            };
        }
    }
}
