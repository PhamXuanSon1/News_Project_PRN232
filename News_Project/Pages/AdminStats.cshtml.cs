using Microsoft.AspNetCore.Mvc.RazorPages;
using News_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace News_Project.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminStatsModel : PageModel
    {
        private readonly NewsDbContext _context;
        public AdminStatsModel(NewsDbContext context)
        {
            _context = context;
        }

        public StatsDto Stats { get; set; }
        public string ErrorMessage { get; set; }

        public class StatsDto
        {
            public int TotalUsers { get; set; }
            public int TotalNews { get; set; }
            public int TotalComments { get; set; }
            public int TotalMedia { get; set; }
            public int TotalCategories { get; set; }
            public int TotalTags { get; set; }
        }

        public async Task OnGetAsync()
        {
            try
            {
                Stats = new StatsDto
                {
                    TotalUsers = await _context.Users.CountAsync(),
                    TotalNews = await _context.News.CountAsync(),
                    TotalComments = await _context.Comments.CountAsync(),
                    TotalMedia = await _context.Media.CountAsync(),
                    TotalCategories = await _context.Categories.CountAsync(),
                    TotalTags = await _context.Tags.CountAsync()
                };
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
} 