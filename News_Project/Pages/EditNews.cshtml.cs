using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace News_Project.Pages
{
    public class EditNewsModel : PageModel
    {
        private readonly NewsDbContext _context;
        public EditNewsModel(NewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public News NewsItem { get; set; }
        public List<Category> Categories { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            NewsItem = await _context.News.FindAsync(id);
            if (NewsItem == null) return NotFound();
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var news = await _context.News.FindAsync(NewsItem.NewsId);
            if (news == null) return NotFound();
            news.Title = NewsItem.Title;
            news.Summary = NewsItem.Summary;
            news.Content = NewsItem.Content;
            news.CategoryId = NewsItem.CategoryId;
            await _context.SaveChangesAsync();
            Message = "Đã lưu thay đổi.";
            Categories = await _context.Categories.ToListAsync();
            NewsItem = news;
            return Page();
        }
    }
} 