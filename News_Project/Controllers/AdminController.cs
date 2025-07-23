using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public AdminController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Admin/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalNews = await _context.News.CountAsync(),
                TotalComments = await _context.Comments.CountAsync(),
                TotalMedia = await _context.Media.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                TotalTags = await _context.Tags.CountAsync()
            };
            return Ok(stats);
        }

        // DELETE: api/Admin/news/5
        [HttpDelete("news/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Admin/user/5
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Admin/comment/5
        [HttpDelete("comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Admin/media/5
        [HttpDelete("media/{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            var media = await _context.Media.FindAsync(id);
            if (media == null) return NotFound();
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 