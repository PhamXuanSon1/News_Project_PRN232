using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsDetailController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public NewsDetailController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDTO>> Get(int id)
        {
            var n = await _context.News.Include(x => x.Author).Include(x => x.Category).FirstOrDefaultAsync(x => x.NewsId == id);
            if (n == null) return NotFound();
            // Tăng lượt xem
            n.Views++;
            await _context.SaveChangesAsync();
            return new NewsDTO
            {
                NewsId = n.NewsId,
                Title = n.Title,
                Content = n.Content,
                Summary = n.Summary,
                AuthorId = n.AuthorId,
                AuthorName = n.Author.UserName,
                CategoryId = n.CategoryId,
                CategoryName = n.Category.Name,
                PublishedAt = n.PublishedAt,
                Status = n.Status,
                Views = n.Views
            };
        }
    }
} 