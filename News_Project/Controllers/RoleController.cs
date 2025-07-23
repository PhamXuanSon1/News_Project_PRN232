using Microsoft.AspNetCore.Mvc;
using News_Project.Models;
using News_Project.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace News_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly NewsDbContext _context;
        public RoleController(NewsDbContext context)
        {
            _context = context;
        }

        // GET: api/Role/users?role=admin
        [HttpGet("users")]
        [Authorize(Policy = "RequireEditorRole")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByRole([FromQuery] string role)
        {
            var users = await _context.Users.Where(u => u.Role == role).ToListAsync();
            return users.Select(u => new UserDTO
            {
                UserId = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        // PUT: api/Role/5
        [HttpPut("{userId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] string role)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();
            user.Role = role;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 