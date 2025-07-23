using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace News_Project.Pages
{
    [Authorize]
    public class UserProfileModel : PageModel
    {
        private readonly NewsDbContext _context;
        public UserProfileModel(NewsDbContext context)
        {
            _context = context;
        }

        public User UserInfo { get; set; }
        public List<News> NewsList { get; set; }
        public List<Comment> Comments { get; set; }

        public async Task OnGetAsync()
        {
            var email = User.Identity.Name;
            UserInfo = await _context.Users.FirstOrDefaultAsync(u => u.UserName == email || u.Email == email);
            if (UserInfo != null)
            {
                NewsList = await _context.News
                    .Where(n => n.AuthorId == UserInfo.Id)
                    .OrderByDescending(n => n.PublishedAt)
                    .ToListAsync();

                Comments = await _context.Comments
                    .Include(c => c.News)
                    .Where(c => c.UserId == UserInfo.Id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
        }
    }
} 