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
    public class AdminTagsModel : PageModel
    {
        private readonly NewsDbContext _context;
        public AdminTagsModel(NewsDbContext context)
        {
            _context = context;
        }

        public List<Tag> Tags { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Tags = await _context.Tags.OrderBy(t => t.TagId).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var tag = new Tag { Name = name };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                Message = $"Đã thêm tag: {name}";
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                Message = $"Đã xóa tag: {tag.Name}";
            }
            await OnGetAsync();
            return Page();
        }
    }
} 