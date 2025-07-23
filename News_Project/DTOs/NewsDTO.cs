namespace News_Project.DTOs
{
    public class NewsDTO
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Status { get; set; }
        public int Views { get; set; }
    }
} 