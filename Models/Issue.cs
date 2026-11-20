
using System.ComponentModel.DataAnnotations;

namespace Issue_tracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public int ReporterId { get; set; }
        public AppUser Reporter { get; set; } = null!;
        public int? AssigneeId { get; set; }
        public AppUser? Assignee { get; set; }

    }
}