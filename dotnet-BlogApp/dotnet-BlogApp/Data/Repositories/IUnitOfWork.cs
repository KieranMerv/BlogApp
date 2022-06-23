namespace dotnet_BlogApp.Data.Repositories
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepo { get; }
        Task<int> SaveAsync();
    }
}
