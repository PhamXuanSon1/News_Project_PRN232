using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public MediaController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Media/news/5
        [HttpGet("news/{newsId}")]
        public async Task<ActionResult<IEnumerable<MediaDTO>>> GetByNews(int newsId)
        {
            var media = await _context.Media.Where(m => m.NewsId == newsId).ToListAsync();
            return media.Select(m => new MediaDTO
            {
                MediaId = m.MediaId,
                Url = m.Url,
                Type = m.Type,
                NewsId = m.NewsId
            }).ToList();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaDTO>> Get(int id)
        {
            var m = await _context.Media.FindAsync(id);
            if (m == null) return NotFound();
            return new MediaDTO
            {
                MediaId = m.MediaId,
                Url = m.Url,
                Type = m.Type,
                NewsId = m.NewsId
            };
        }

        // POST: api/Media
        [HttpPost]
        public async Task<ActionResult<MediaDTO>> Create(MediaDTO dto)
        {
            var media = new Media
            {
                Url = dto.Url,
                Type = dto.Type,
                NewsId = dto.NewsId
            };
            _context.Media.Add(media);
            await _context.SaveChangesAsync();
            dto.MediaId = media.MediaId;
            return CreatedAtAction(nameof(Get), new { id = media.MediaId }, dto);
        }

        // PUT: api/Media/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MediaDTO dto)
        {
            var media = await _context.Media.FindAsync(id);
            if (media == null) return NotFound();
            media.Url = dto.Url;
            media.Type = dto.Type;
            media.NewsId = dto.NewsId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var media = await _context.Media.FindAsync(id);
            if (media == null) return NotFound();
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 