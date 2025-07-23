using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace News_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly NewsDbContext _context;
        public IndexModel(NewsDbContext context)
        {
            _context = context;
        }

        public List<News> NewsList { get; set; }
        public List<News> YesterdayNews { get; set; }
        public List<News> WeeklyTrending { get; set; }
        public List<News> Top10News { get; set; }

        public async Task OnGetAsync()
        {
            var weekAgo = DateTime.UtcNow.AddDays(-7);
            var yesterday = DateTime.Today.AddDays(-1);
            NewsList = await _context.News
                .Include(n => n.Media)
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Where(n => n.Status == "published")
                .OrderByDescending(n => n.PublishedAt)
                .Take(12)
                .ToListAsync();
            YesterdayNews = await _context.News
                .Where(n => n.PublishedAt.Date == yesterday && n.Status == "published")
                .OrderByDescending(n => n.Views)
                .Take(5)
                .ToListAsync();
            WeeklyTrending = await _context.News
                .Where(n => n.PublishedAt >= weekAgo && n.Status == "published")
                .OrderByDescending(n => n.Views)
                .Take(5)
                .ToListAsync();
            Top10News = await _context.News
                .Where(n => n.PublishedAt >= weekAgo && n.Status == "published")
                .OrderByDescending(n => n.Views)
                .Take(10)
                .ToListAsync();
        }
    }
} 