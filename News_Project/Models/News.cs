namespace News_Project.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Status { get; set; }
        public int Views { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
        public ICollection<Media> Media { get; set; }
    }
} 