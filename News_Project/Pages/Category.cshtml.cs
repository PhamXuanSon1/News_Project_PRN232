using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace News_Project.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly NewsDbContext _context;
        public CategoryModel(NewsDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }
        public List<News> NewsList { get; set; }

        public async Task OnGetAsync(int id)
        {
            Category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (Category != null)
            {
                NewsList = await _context.News
                    .Include(n => n.Media)
                    .Include(n => n.Author)
                    .Where(n => n.CategoryId == id)
                    .OrderByDescending(n => n.PublishedAt)
                    .ToListAsync();
            }
        }
    }
} 