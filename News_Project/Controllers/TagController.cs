using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public TagController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAll()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(t => new TagDTO
            {
                TagId = t.TagId,
                Name = t.Name
            }).ToList();
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> Get(int id)
        {
            var t = await _context.Tags.FindAsync(id);
            if (t == null) return NotFound();
            return new TagDTO
            {
                TagId = t.TagId,
                Name = t.Name
            };
        }

        // POST: api/Tag
        [HttpPost]
        public async Task<ActionResult<TagDTO>> Create(TagDTO dto)
        {
            var tag = new Tag
            {
                Name = dto.Name
            };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            dto.TagId = tag.TagId;
            return CreatedAtAction(nameof(Get), new { id = tag.TagId }, dto);
        }

        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TagDTO dto)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return NotFound();
            tag.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return NotFound();
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 