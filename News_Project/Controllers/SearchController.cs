using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public SearchController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Search?keyword=abc&categoryId=1&tagId=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> Search([FromQuery] string? keyword, [FromQuery] int? categoryId, [FromQuery] int? tagId)
        {
            var query = _context.News
                .Include(n => n.Author)
                .Include(n => n.Category)
                .Include(n => n.NewsTags).ThenInclude(nt => nt.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(n => n.Title.Contains(keyword) || n.Content.Contains(keyword) || n.Summary.Contains(keyword));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(n => n.CategoryId == categoryId);
            }
            if (tagId.HasValue)
            {
                query = query.Where(n => n.NewsTags.Any(nt => nt.TagId == tagId));
            }

            var news = await query.ToListAsync();
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