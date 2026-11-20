using Issue_tracker.Models;
using Microsoft.EntityFrameworkCore;
namespace Issue_tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<AppUser> AppUsers {  get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().HasOne(x => x.Reporter).WithMany().HasForeignKey(x => x.ReporterId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Issue>().HasOne(x => x.Assignee).WithMany().HasForeignKey(x => x.AssigneeId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>().HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
