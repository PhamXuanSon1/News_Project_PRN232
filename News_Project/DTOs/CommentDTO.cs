namespace News_Project.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 