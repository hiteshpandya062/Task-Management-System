using DataAccessLayer;
using DataAccessLayer.ViewModels;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Services
{
    public class TaskServiceBAL : ITaskServiceBAL
    {
        private readonly ITaskServiceDAL _taskServiceDAL;
        public TaskServiceBAL(ITaskServiceDAL taskServiceDAL)
        {
            _taskServiceDAL = taskServiceDAL;
        }

        public async Task<ApiResponse<bool>> AddTask(AddUpdateTaskDetailsVM taskDetail)
        {
            TaskDetail tasks = new TaskDetail()
            {
                title = taskDetail.Title,
                description = taskDetail.Description,
                assigneeId = taskDetail.AssigneeId,
                createdAt = DateTime.Now,
                creatorId = taskDetail.CreatorId,
                priority = taskDetail.Priority,
                status = taskDetail.Status,
                updatedAt = null,
            };

            bool result = await _taskServiceDAL.AddTask(tasks);

            if (result)
                return new ApiResponse<bool>
                {
                    Errors = new List<string>(),
                    Message = "Tasks added successfully.",
                    Result = true,
                    StatusCode = StatusCodes.Status200OK
                };
            else
                return new ApiResponse<bool>
                {
                    Errors = new List<string>() { "Error while adding tasks." },
                    Message = "Error while adding tasks.",
                    Result = false,
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }

        public async Task<ApiListResponse<List<TaskDetailsVM>>> GetTaskDetailsByStatusAndAssignee(Status? status, int? assignee)
        {
            var details = await _taskServiceDAL.GetTaskDetailsByStatusAndAssignee(status, assignee);

            return new ApiListResponse<List<TaskDetailsVM>>
            {
                Errors = new List<string>(),
                Message = "Task List found.",
                Result = details,
                StatusCode = StatusCodes.Status200OK,
                TotalCount = details.Count,
            };

        }

        public async Task<ApiResponse<bool>> UpdateTask(int id, AddUpdateTaskDetailsVM taskDetails)
        {
            if (id == null || id == 0)
            {
                return new ApiResponse<bool>
                {
                    Errors = new List<string>() { "Error while updating tasks as id is null." },
                    Message = "Error while updating tasks as id is null.",
                    Result = false,
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            var getTasks = await _taskServiceDAL.GetTaskById(id);
            if (getTasks != null)
            {
                TaskDetail task = new TaskDetail()
                {
                    id = id,
                    status = taskDetails.Status,
                    priority = taskDetails.Priority,
                    assigneeId = taskDetails.AssigneeId,
                    createdAt = getTasks.CreatedAt,
                    creatorId = taskDetails.CreatorId,
                    description = taskDetails.Description,
                    title = taskDetails.Title,
                    updatedAt = DateTime.Now,
                };

                var result = await _taskServiceDAL.UpdateTask(task);
                if (result)
                    return new ApiResponse<bool>
                    {
                        Errors = new List<string>(),
                        Message = "Tasks updated successfully.",
                        Result = true,
                        StatusCode = StatusCodes.Status200OK
                    };
                else
                    return new ApiResponse<bool>
                    {
                        Errors = new List<string>() { "Error while updating tasks." },
                        Message = "Error while updating tasks.",
                        Result = false,
                        StatusCode = StatusCodes.Status400BadRequest
                    };
            }
            else
                return new ApiResponse<bool>
                {
                    Errors = new List<string>() { "Error while updating tasks as task not found." },
                    Message = "Error while updating tasks as task not found.",
                    Result = false,
                    StatusCode = StatusCodes.Status404NotFound
                };
        }

        public async Task<ApiResponse<bool>> DeleteTask(int id)
        {
            var result = await _taskServiceDAL.DeleteTask(id);
            if (result)
                return new ApiResponse<bool>
                {
                    Errors = new List<string>(),
                    Message = "Task deleted successfully.",
                    Result = true,
                    StatusCode = StatusCodes.Status200OK
                };
            else
                return new ApiResponse<bool>
                {
                    Errors = new List<string>() { "Error while deleting tasks." },
                    Message = "Error while deleting tasks.",
                    Result = false,
                    StatusCode = StatusCodes.Status400BadRequest
                };
        }
    }
}
