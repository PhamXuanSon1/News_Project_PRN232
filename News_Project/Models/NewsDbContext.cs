using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace News_Project.Models
{
    public class NewsDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cấu hình quan hệ nhiều-nhiều cho NewsTag
            modelBuilder.Entity<NewsTag>()
                .HasKey(nt => new { nt.NewsId, nt.TagId });

            // Cấu hình lại quan hệ Comment-User để tránh cascade delete
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 