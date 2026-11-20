namespace Issue_tracker.Models
{
    public class Comment
    {
       public int Id { get; set; }
        public string Text { get; set; } = "";
        public int IssueId { get; set; }
        public Issue Issue { get; set; } = null!;
        public int AuthorId { get; set; }
        public AppUser Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}
