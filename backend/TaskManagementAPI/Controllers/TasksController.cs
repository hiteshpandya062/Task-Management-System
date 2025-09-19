using DataAccessLayer;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskServiceBAL _taskServiceBAL;
        public TasksController(ITaskServiceBAL taskServiceBAL)
        {
            _taskServiceBAL = taskServiceBAL;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> AddTask([FromBody] AddUpdateTaskDetailsVM taskDetails)
        {
            var apiResponse = await _taskServiceBAL.AddTask(taskDetails);
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(apiResponse),
                StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                StatusCodes.Status404NotFound => NotFound(apiResponse),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                _ => BadRequest(apiResponse),
            };
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<bool>>> GetTasksList([FromQuery] Status? status, [FromQuery] int? assignee)
        {
            try
            {
                var apiResponse = await _taskServiceBAL.GetTaskDetailsByStatusAndAssignee(status, assignee);
                return apiResponse.StatusCode switch
                {
                    StatusCodes.Status200OK => Ok(apiResponse),
                    StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                    StatusCodes.Status404NotFound => NotFound(apiResponse),
                    StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                    _ => BadRequest(apiResponse),
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTask(int id, [FromBody] AddUpdateTaskDetailsVM taskDetails)
        {
            var apiResponse = await _taskServiceBAL.UpdateTask(id, taskDetails);
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(apiResponse),
                StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                StatusCodes.Status404NotFound => NotFound(apiResponse),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, apiResponse),
                _ => BadRequest(apiResponse),
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTask(int id)
        {
            var apiResponse = await _taskServiceBAL.DeleteTask(id);
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