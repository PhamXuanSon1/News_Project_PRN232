namespace News_Project.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
} 