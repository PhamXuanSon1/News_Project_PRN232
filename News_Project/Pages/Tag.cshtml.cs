using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace News_Project.Pages
{
    public class TagModel : PageModel
    {
        private readonly NewsDbContext _context;
        public TagModel(NewsDbContext context)
        {
            _context = context;
        }

        public Tag Tag { get; set; }
        public List<News> NewsList { get; set; }

        public async Task OnGetAsync(int id)
        {
            Tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagId == id);
            if (Tag != null)
            {
                NewsList = await _context.NewsTags
                    .Where(nt => nt.TagId == id)
                    .Select(nt => nt.News)
                    .OrderByDescending(n => n.PublishedAt)
                    .ToListAsync();
            }
        }
    }
} 