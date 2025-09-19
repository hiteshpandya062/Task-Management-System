using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Interface;

namespace TaskManagementAPI.Services
{
    public class AuthServiceDAL : IAuthServiceDAL
    {
        private readonly AppDbContext _dbContext;
        public AuthServiceDAL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Register(User userDetails)
        {
            _dbContext.Add(userDetails);
            _dbContext.Entry(userDetails).State = EntityState.Added;
            var data = await _dbContext.SaveChangesAsync();
            return data > 0;
        }
    }
}
