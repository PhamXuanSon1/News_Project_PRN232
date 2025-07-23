using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace News_Project.Pages
{
    public class SearchModel : PageModel
    {
        private readonly NewsDbContext _context;
        public SearchModel(NewsDbContext context)
        {
            _context = context;
        }

        public string Keyword { get; set; }
        public List<News> NewsList { get; set; }

        public async Task OnGetAsync(string keyword)
        {
            Keyword = keyword;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                NewsList = await _context.News
                    .Include(n => n.Media)
                    .Include(n => n.Category)
                    .Include(n => n.Author)
                    .Where(n => (n.Title.Contains(keyword) || n.Summary.Contains(keyword) || n.Content.Contains(keyword)) && n.Status == "published")
                    .OrderByDescending(n => n.PublishedAt)
                    .ToListAsync();
            }
            else
            {
                NewsList = new List<News>();
            }
        }
    }
} 