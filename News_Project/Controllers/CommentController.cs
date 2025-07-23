using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public CommentController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Comment/news/5
        [HttpGet("news/{newsId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetByNews(int newsId)
        {
            var comments = await _context.Comments.Include(c => c.User).Where(c => c.NewsId == newsId).ToListAsync();
            return comments.Select(c => new CommentDTO
            {
                CommentId = c.CommentId,
                NewsId = c.NewsId,
                UserId = c.UserId,
                UserName = c.User.UserName,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> Get(int id)
        {
            var c = await _context.Comments.Include(x => x.User).FirstOrDefaultAsync(x => x.CommentId == id);
            if (c == null) return NotFound();
            return new CommentDTO
            {
                CommentId = c.CommentId,
                NewsId = c.NewsId,
                UserId = c.UserId,
                UserName = c.User.UserName,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            };
        }

        // POST: api/Comment
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> Create(CommentDTO dto)
        {
            var comment = new Comment
            {
                NewsId = dto.NewsId,
                UserId = dto.UserId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            dto.CommentId = comment.CommentId;
            dto.CreatedAt = comment.CreatedAt;
            return CreatedAtAction(nameof(Get), new { id = comment.CommentId }, dto);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CommentDTO dto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            comment.Content = dto.Content;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 