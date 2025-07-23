using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public CategoryController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();
            return new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            };
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            dto.CategoryId = category.CategoryId;
            return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, dto);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            category.Name = dto.Name;
            category.Description = dto.Description;
            category.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpGet("count")]
        //public async Task<IActionResult> GetCategoryCount()
        //{
        //    var category = _context.Categories
        //        .Include(c => c.News)
        //             .ThenInclude(c => c.Comments)
        //        .Select(s => new
        //        {
        //            CategoryId = s.CategoryId,
        //            Name = s.Name,
        //            Description = s.Description,
        //            CreatedAt = s.CreatedAt,
        //            newcount = s.News.Count,
        //        })
        //        .ToList();

        //    return Ok(category);
        //}
    }
} 