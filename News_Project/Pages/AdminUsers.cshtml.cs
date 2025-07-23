using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace News_Project.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminUsersModel : PageModel
    {
        private readonly NewsDbContext _context;
        private readonly UserManager<User> _userManager;
        public AdminUsersModel(NewsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<User> Users { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<IActionResult> OnPostChangeRoleAsync(int userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null && !string.IsNullOrWhiteSpace(role))
            {
                try
                {
                    // Xóa hết role cũ
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    // Gán role mới
                    await _userManager.AddToRoleAsync(user, role);

                    // Nếu muốn lưu role vào property User.Role để hiển thị
                    user.Role = role;
                    await _userManager.UpdateAsync(user);

                    Message = $"Đã đổi quyền user {user.UserName} thành {role}.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "User đã bị thay đổi hoặc xóa. Vui lòng tải lại trang.");
                }
            }
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostToggleLockAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.LockoutEnabled = !user.LockoutEnabled;
                await _context.SaveChangesAsync();
                Message = user.LockoutEnabled ? $"Đã khóa user {user.UserName}." : $"Đã mở khóa user {user.UserName}.";
            }
            await OnGetAsync();
            return Page();
        }
    }
} 