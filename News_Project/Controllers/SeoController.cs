using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeoController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public SeoController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Seo/news/5
        [HttpGet("news/{id}")]
        public async Task<ActionResult<object>> GetSeoInfo(int id)
        {
            var n = await _context.News.FindAsync(id);
            if (n == null) return NotFound();
            // Tạo friendly url từ title
            string friendlyUrl = n.Title.ToLower().Replace(" ", "-").Replace(".", "").Replace(",", "");
            return Ok(new {
                Title = n.Title,
                Description = n.Summary,
                FriendlyUrl = $"/news/{n.NewsId}/{friendlyUrl}"
            });
        }
    }
} 