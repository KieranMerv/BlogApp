using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;
using Microsoft.EntityFrameworkCore;

namespace dotnet_BlogApp.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task Add(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public void Update(PostVM postVM, Post post)
        {
            if (post.Title != postVM.Title) post.Title = postVM.Title;
            if (post.Body != postVM.Body) post.Title = postVM.Title;
            if (post.Updated != postVM.Updated) post.Updated = postVM.Updated;

            // Not needed! Causes concurrency problems.
            // _context.Posts.Update(post);
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}
