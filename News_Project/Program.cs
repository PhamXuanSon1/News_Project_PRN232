using Microsoft.EntityFrameworkCore;
using News_Project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, NoOpEmailSender>();

builder.Services.AddDbContext<News_Project.Models.NewsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewsDbContext")));
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<NewsDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();

app.Run();

