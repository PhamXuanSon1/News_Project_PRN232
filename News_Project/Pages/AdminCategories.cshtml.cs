using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace News_Project.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminCategoriesModel : PageModel
    {
        private readonly NewsDbContext _context;
        public AdminCategoriesModel(NewsDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.OrderBy(c => c.CategoryId).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string name, string description)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
            {
                var cat = new Category
                {
                    Name = name,
                    Description = description,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Categories.Add(cat);
                await _context.SaveChangesAsync();
                Message = $"Đã thêm chuyên mục: {name}";
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int categoryId)
        {
            var cat = await _context.Categories.FindAsync(categoryId);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
                Message = $"Đã xóa chuyên mục: {cat.Name}";
            }
            await OnGetAsync();
            return Page();
        }
    }
} 