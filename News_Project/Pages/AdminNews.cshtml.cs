using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace News_Project.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminNewsModel : PageModel
    {
        private readonly NewsDbContext _context;
        public AdminNewsModel(NewsDbContext context)
        {
            _context = context;
        }

        public List<News> NewsList { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            NewsList = await _context.News
                .Include(n => n.Author)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                Message = $"Đã xóa bài viết: {news.Title}";
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostApproveAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news != null && news.Status != "published")
            {
                news.Status = "published";
                await _context.SaveChangesAsync();
                Message = $"Đã duyệt bài viết: {news.Title}";
            }
            await OnGetAsync();
            return Page();
        }
    }
} 