using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public ReportController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Report/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = new
            {
                TotalViews = await _context.News.SumAsync(n => n.Views),
                TotalNews = await _context.News.CountAsync(),
                TotalComments = await _context.Comments.CountAsync(),
                TotalUsers = await _context.Users.CountAsync()
            };
            return Ok(result);
        }

        // GET: api/Report/top-news?count=5
        [HttpGet("top-news")]
        public async Task<IActionResult> GetTopNews([FromQuery] int count = 5)
        {
            var news = await _context.News
                .OrderByDescending(n => n.Views)
                .Take(count)
                .Select(n => new { n.NewsId, n.Title, n.Views })
                .ToListAsync();
            return Ok(news);
        }

        // GET: api/Report/top-users?count=5
        [HttpGet("top-users")]
        public async Task<IActionResult> GetTopUsers([FromQuery] int count = 5)
        {
            var users = await _context.Users
                .OrderByDescending(u => u.News.Count)
                .Take(count)
                .Select(u => new { u.Id, u.UserName, NewsCount = u.News.Count })
                .ToListAsync();
            return Ok(users);
        }
    }
} 