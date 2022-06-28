using dotnet_BlogApp.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_BlogApp.Data
{
    public class BlogDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
#pragma warning disable CS8618
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
#pragma warning restore CS8618

        public DbSet<Post> Posts { get; set; }
    }
}
