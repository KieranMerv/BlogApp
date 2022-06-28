using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Models.View;

namespace dotnet_BlogApp.Data.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPublicPosts();
        Task<IEnumerable<Post>> GetAllUserPosts(string userId);
        Task<Post?> GetById(Guid Id);
        Task Add(Post post);
        void Update(PostAddEditVM postAddEditVM, Post post);
        void Delete(Post post);
        Task DeleteAllUserPosts(string id);
    }
}
