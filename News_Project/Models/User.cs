using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace News_Project.Models
{
    public class User : IdentityUser<int>
    {
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
} 