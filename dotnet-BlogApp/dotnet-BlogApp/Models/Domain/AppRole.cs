using Microsoft.AspNetCore.Identity;

namespace dotnet_BlogApp.Models.Domain
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName) { }

        // Not needed, Microsoft Identity implements relationship to users automatically.
        // public ICollection<AppUser>? AppUsers { get; set; }
    }
}
