using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsListController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public NewsListController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsList/latest?count=10
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetLatest([FromQuery] int count = 10)
        {
            var news = await _context.News
                .Include(n => n.Author)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishedAt)
                .Take(count)
                .ToListAsync();
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

        // GET: api/NewsList/featured?count=10
        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetFeatured([FromQuery] int count = 10)
        {
            var news = await _context.News
                .Include(n => n.Author)
                .Include(n => n.Category)
                .OrderByDescending(n => n.Views)
                .Take(count)
                .ToListAsync();
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
    }
} 