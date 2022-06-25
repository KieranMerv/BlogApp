using dotnet_BlogApp.Models.Domain;

namespace dotnet_BlogApp.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser appUser);
    }
}
