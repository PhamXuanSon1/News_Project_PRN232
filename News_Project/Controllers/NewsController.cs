using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public NewsController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetAll()
        {
            var news = await _context.News.Include(n => n.Author).Include(n => n.Category).ToListAsync();
            return news.Select(n => new NewsDTO
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
            }).ToList();
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDTO>> Get(int id)
        {
            var n = await _context.News.Include(x => x.Author).Include(x => x.Category).FirstOrDefaultAsync(x => x.NewsId == id);
            if (n == null) return NotFound();
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

        // POST: api/News
        [HttpPost]
        public async Task<ActionResult<NewsDTO>> Create(NewsDTO dto)
        {
            var news = new News
            {
                Title = dto.Title,
                Content = dto.Content,
                Summary = dto.Summary,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                PublishedAt = dto.PublishedAt,
                Status = dto.Status,
                Views = dto.Views
            };
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            dto.NewsId = news.NewsId;
            return CreatedAtAction(nameof(Get), new { id = news.NewsId }, dto);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NewsDTO dto)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            news.Title = dto.Title;
            news.Content = dto.Content;
            news.Summary = dto.Summary;
            news.AuthorId = dto.AuthorId;
            news.CategoryId = dto.CategoryId;
            news.PublishedAt = dto.PublishedAt;
            news.Status = dto.Status;
            news.Views = dto.Views;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 