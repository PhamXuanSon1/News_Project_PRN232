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
    public class AdminMediaModel : PageModel
    {
        private readonly NewsDbContext _context;
        public AdminMediaModel(NewsDbContext context)
        {
            _context = context;
        }

        public List<Media> MediaList { get; set; }
        public List<News> NewsList { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            MediaList = await _context.Media.Include(m => m.News).OrderBy(m => m.MediaId).ToListAsync();
            NewsList = await _context.News.OrderByDescending(n => n.PublishedAt).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string url, string type, int newsId)
        {
            if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(type) && newsId > 0)
            {
                var media = new Media { Url = url, Type = type, NewsId = newsId };
                _context.Media.Add(media);
                await _context.SaveChangesAsync();
                Message = $"Đã thêm media cho bài viết ID: {newsId}";
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int mediaId)
        {
            var media = await _context.Media.FindAsync(mediaId);
            if (media != null)
            {
                _context.Media.Remove(media);
                await _context.SaveChangesAsync();
                Message = $"Đã xóa media ID: {mediaId}";
            }
            await OnGetAsync();
            return Page();
        }
    }
} 