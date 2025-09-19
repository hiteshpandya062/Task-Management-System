using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceBAL _authServiceBAL;
        public AuthController(IAuthServiceBAL authServiceBAL)
        {
            _authServiceBAL = authServiceBAL;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<bool>>> Register([FromBody] UsersRegisterVM usersRegister)
        {
            var apiResponse = await _authServiceBAL.Register(usersRegister);
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(apiResponse),
                StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                StatusCodes.Status404NotFound => NotFound(apiResponse),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                _ => BadRequest(apiResponse),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<bool>>> Login([FromBody] DataAccessLayer.ViewModels.LoginRequest request)
        {
            try
            {
                var apiResponse = await _authServiceBAL.Login(request.email, request.password);
                return apiResponse.StatusCode switch
                {
                    StatusCodes.Status200OK => Ok(apiResponse),
                    StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                    StatusCodes.Status404NotFound => NotFound(apiResponse),
                    StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                    _ => BadRequest(apiResponse),
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
