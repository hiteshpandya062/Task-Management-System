using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Interface;

namespace TaskManagementAPI.Services
{
    public class UserServiceDAL : IUserServiceDAL
    {
        private readonly AppDbContext _dbContext;
        public UserServiceDAL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User userDetails = await _dbContext.Users.Where(x => x.email == email).FirstOrDefaultAsync();
            return userDetails ?? null;
        }
        public async Task<List<User>> GetUsersForTaskAssignment()
        {
            List<User> userDetails = await _dbContext.Users
                .ToListAsync(); ;
            return userDetails ?? null;
        }
    }
}