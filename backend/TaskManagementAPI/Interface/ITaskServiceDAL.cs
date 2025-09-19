using DataAccessLayer;
using DataAccessLayer.ViewModels;

namespace TaskManagementAPI.Interface
{
    public interface ITaskServiceDAL
    {
        Task<bool> AddTask(TaskDetail taskDetail);
        Task<TaskDetailsVM> GetTaskById(int id);
        Task<bool> UpdateTask(TaskDetail taskDetail);
        Task<List<TaskDetailsVM>> GetTaskDetailsByStatusAndAssignee(Status? status, int? assignee);
        Task<bool> DeleteTask(int id);
    }
}
