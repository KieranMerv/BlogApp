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

        public void Update(PostAddEditVM postAddEditVM, Post post)
        {
            bool postChanged = false;

            if (post.Title != postAddEditVM.Title)
            {
                post.Title = postAddEditVM.Title;
                postChanged = true;
            }

            if (post.Body != postAddEditVM.Body)
            {
                post.Body = postAddEditVM.Body;
                postChanged = true;
            }

            if (post.IsPrivate != postAddEditVM.IsPrivate)
            {
                post.IsPrivate = postAddEditVM.IsPrivate;
                postChanged = true;
            }

            if (postChanged == true) post.Updated = DateTime.Now;

            // Not needed!
            // _context.Posts.Update(post);
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}
