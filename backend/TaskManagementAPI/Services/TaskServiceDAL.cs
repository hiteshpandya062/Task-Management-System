using DataAccessLayer;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Interface;

namespace TaskManagementAPI.Services
{
    public class TaskServiceDAL : ITaskServiceDAL
    {
        private readonly AppDbContext _dbContext;
        public TaskServiceDAL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskDetailsVM> GetTaskById(int id)
        {
            var data = await _dbContext.TaskDetails
                .Join(_dbContext.Users, td => td.assigneeId, u => u.id, (td, u) => new { td, u })
                .Join(_dbContext.Users, tdu => tdu.td.creatorId, c => c.id,
                    (tdu, c) => new TaskDetailsVM
                    {
                        Title = tdu.td.title,
                        Description = tdu.td.description,
                        Status = tdu.td.status,
                        Priority = tdu.td.priority,
                        CreatedAt = tdu.td.createdAt,
                        UpdatedAt = tdu.td.updatedAt,
                        AssigneeName = tdu.u.username,
                        CreatorName = c.username
                    })
                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> AddTask(TaskDetail taskDetail)
        {
            _dbContext.Add(taskDetail);
            _dbContext.Entry(taskDetail).State = EntityState.Added;
            var data = await _dbContext.SaveChangesAsync();
            return data > 0;
        }

        public async Task<bool> UpdateTask(TaskDetail taskDetail)
        {
            _dbContext.Update(taskDetail);
            _dbContext.Entry(taskDetail).State = EntityState.Modified;
            var data = await _dbContext.SaveChangesAsync();
            return data > 0;
        }

        public async Task<List<TaskDetailsVM>> GetTaskDetailsByStatusAndAssignee(Status? status, int? assignee)
        {
            var query = _dbContext.TaskDetails.AsQueryable();

            if (status.HasValue)
                query = query.Where(t => t.status == status.Value);

            if (assignee.HasValue)
                query = query.Where(t => t.assigneeId == assignee.Value);

            var tasks = await query
                .Join(_dbContext.Users, td => td.assigneeId, u => u.id, (td, u) => new { td, u })
                .Join(_dbContext.Users, tdu => tdu.td.creatorId, c => c.id,
                    (tdu, c) => new TaskDetailsVM
                    {
                        Title = tdu.td.title,
                        Description = tdu.td.description,
                        Status = tdu.td.status,
                        Priority = tdu.td.priority,
                        CreatedAt = tdu.td.createdAt,
                        UpdatedAt = tdu.td.updatedAt,
                        AssigneeName = tdu.u.username,
                        CreatorName = c.username
                    })
                .ToListAsync();
            return tasks;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _dbContext.TaskDetails.FindAsync(id);
            if (task == null)
                return false;
            _dbContext.TaskDetails.Remove(task);
            var data = await _dbContext.SaveChangesAsync();
            return data > 0;
        }
    }
}
