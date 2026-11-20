using Issue_tracker.Data;
using Issue_tracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
namespace Issue_tracker.Services
{
    public class IssueService : IIssueService
    {
        private readonly AppDbContext _context;

        public IssueService(AppDbContext context)
        {
            _context = context;
        }
        public string? ChangeStatus(int id , Status newStatus)
        {
            var issue = _context.Issues.Find(id);
            if (issue == null) 
            {
                return "Issue cant found";
            }
            issue.Status = newStatus;
            _context.SaveChanges();
            return null;

        }
    }
}
