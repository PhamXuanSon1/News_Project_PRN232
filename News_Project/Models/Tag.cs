namespace News_Project.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; }
    }
} 