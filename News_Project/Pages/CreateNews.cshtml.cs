using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace News_Project.Pages
{
    [Authorize(Roles = "editor,admin")]
    public class CreateNewsModel : PageModel
    {
        private readonly NewsDbContext _context;
        public CreateNewsModel(NewsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string Message { get; set; }

        public class InputModel
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Summary { get; set; }
            public int CategoryId { get; set; }
            public string ImageUrl { get; set; }
        }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Categories = await _context.Categories
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name })
                .ToListAsync();
            if (!ModelState.IsValid)
                return Page();

            var email = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == email || u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy user.");
                return Page();
            }

            var news = new News
            {
                Title = Input.Title,
                Content = Input.Content,
                Summary = Input.Summary,
                AuthorId = user.Id,
                CategoryId = Input.CategoryId,
                PublishedAt = DateTime.UtcNow,
                Status = "pending",
                Views = 0
            };
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            // Thêm media nếu có url ảnh
            if (!string.IsNullOrWhiteSpace(Input.ImageUrl))
            {
                var media = new Media
                {
                    Url = Input.ImageUrl,
                    Type = "image",
                    NewsId = news.NewsId
                };
                _context.Media.Add(media);
                await _context.SaveChangesAsync();
            }
            Message = "Đăng bài thành công!";
            ModelState.Clear();
            Input = new InputModel();
            return Page();
        }
    }
} 