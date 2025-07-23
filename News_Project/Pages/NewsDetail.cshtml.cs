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
    public class NewsDetailModel : PageModel
    {
        private readonly NewsDbContext _context;
        public NewsDetailModel(NewsDbContext context)
        {
            _context = context;
        }

        public News News { get; set; }
        public List<Media> MediaList { get; set; }
        public List<Tag> TagList { get; set; }
        public List<Comment> Comments { get; set; }
        public List<News> Top10News { get; set; }
        public int? CurrentUserId { get; set; }
        [BindProperty]
        public string CommentMessage { get; set; }

        public async Task OnGetAsync(int id)
        {
            await LoadData(id);
        }

        public async Task<IActionResult> OnPostAsync(int id, string commentContent)
        {
            await LoadData(id);
            if (!User.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(commentContent))
                return Page();
            var email = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == email || u.Email == email);
            if (user == null) return Page();
            var comment = new Comment
            {
                NewsId = id,
                UserId = user.Id,
                Content = commentContent,
                CreatedAt = DateTime.UtcNow
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            CommentMessage = "Bình luận thành công!";
            await LoadData(id);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(int id, int commentId)
        {
            await LoadData(id);
            var email = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == email || u.Email == email);
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == user.Id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                CommentMessage = "Đã xóa bình luận.";
            }
            await LoadData(id);
            return Page();
        }

        private async Task LoadData(int id)
        {
            News = await _context.News
                .Include(n => n.Author)
                .FirstOrDefaultAsync(n => n.NewsId == id);
            if (News != null)
            {
                MediaList = await _context.Media.Where(m => m.NewsId == id).ToListAsync();
                TagList = await _context.NewsTags
                    .Where(nt => nt.NewsId == id)
                    .Select(nt => nt.Tag)
                    .ToListAsync();
                Comments = await _context.Comments
                    .Include(c => c.User)
                    .Where(c => c.NewsId == id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
                if (User.Identity.IsAuthenticated)
                {
                    var email = User.Identity.Name;
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == email || u.Email == email);
                    CurrentUserId = user?.Id;
                }
                // Lấy top 10 bài nhiều view nhất trong 7 ngày gần nhất
                var weekAgo = DateTime.UtcNow.AddDays(-7);
                Top10News = await _context.News
                    .Where(n => n.PublishedAt >= weekAgo)
                    .OrderByDescending(n => n.Views)
                    .Take(10)
                    .ToListAsync();
            }
        }
    }
} 