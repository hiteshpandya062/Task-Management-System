using DataAccessLayer;
using DataAccessLayer.ViewModels;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Interface
{
    public interface ITaskServiceBAL
    {
        Task<ApiResponse<bool>> AddTask(AddUpdateTaskDetailsVM taskDetail);
        Task<ApiListResponse<List<TaskDetailsVM>>> GetTaskDetailsByStatusAndAssignee(Status? status, int? assignee);
        Task<ApiResponse<bool>> UpdateTask(int id, AddUpdateTaskDetailsVM taskDetails);
        Task<ApiResponse<bool>> DeleteTask(int id);
    }
}
