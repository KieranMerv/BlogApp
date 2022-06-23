using dotnet_BlogApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnet_BlogApp.Data
{
    public class BlogDbContext : DbContext
    {
#pragma warning disable CS8618
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
#pragma warning restore CS8618

        public DbSet<Post> Posts { get; set; }
    }
}
