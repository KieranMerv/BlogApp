using Microsoft.AspNetCore.Identity;

namespace dotnet_BlogApp.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public string KnownAs { get; set; } = "Anonymous";

        public ICollection<Post>? Posts;
    }
}
