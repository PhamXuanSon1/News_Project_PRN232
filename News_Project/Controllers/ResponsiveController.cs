using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsiveController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public ResponsiveController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Responsive/newslist?count=10
        [HttpGet("newslist")]
        public async Task<ActionResult<IEnumerable<object>>> GetNewsList([FromQuery] int count = 10)
        {
            var news = await _context.News
                .OrderByDescending(n => n.PublishedAt)
                .Take(count)
                .Select(n => new {
                    n.NewsId,
                    n.Title,
                    n.Summary,
                    n.PublishedAt,
                    n.Views
                })
                .ToListAsync();
            return Ok(news);
        }

        // GET: api/Responsive/newsdetail/5
        [HttpGet("newsdetail/{id}")]
        public async Task<ActionResult<object>> GetNewsDetail(int id)
        {
            var n = await _context.News.FindAsync(id);
            if (n == null) return NotFound();
            return Ok(new {
                n.NewsId,
                n.Title,
                n.Summary,
                n.PublishedAt,
                n.Views
            });
        }
    }
} 