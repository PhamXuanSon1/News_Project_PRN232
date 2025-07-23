namespace News_Project.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 