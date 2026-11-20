using Issue_tracker.Models;

namespace Issue_tracker.Services
{
    public interface IIssueService
    {
        string? ChangeStatus(int id, Status newStatus);
    }
}
