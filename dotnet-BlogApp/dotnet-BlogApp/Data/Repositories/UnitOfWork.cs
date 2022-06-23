using Microsoft.EntityFrameworkCore;

namespace dotnet_BlogApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;
        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
            PostRepo = new PostRepository(_context);
        }

        public IPostRepository PostRepo { get; private set; }

        public async Task<int> SaveAsync()
        {
            bool saveFailed;
            int saveAsyncInt = 0;

            do
            {
                saveFailed = false;

                try
                {
                    saveAsyncInt = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException concurrencyEx)
                {
                    saveFailed = true;

                    concurrencyEx.Entries.Single().Reload();
                }
            } while (saveFailed);

            return saveAsyncInt;
        }
    }
}
