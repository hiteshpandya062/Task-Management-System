using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TaskDetail> TaskDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskDetail>()
                            .HasOne(t => t.Assignee)
                            .WithMany()
                            .HasForeignKey(t => t.assigneeId)
                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskDetail>()
                .HasOne(t => t.Creator)
                .WithMany()
                .HasForeignKey(t => t.creatorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
