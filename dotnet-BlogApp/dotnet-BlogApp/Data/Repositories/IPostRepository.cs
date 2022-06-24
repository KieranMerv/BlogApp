using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;

namespace dotnet_BlogApp.Data.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post?> GetById(Guid Id);
        Task Add(Post post);
        void Update(PostAddEditVM postAddEditVM, Post post);
        void Delete(Post post);
    }
}
