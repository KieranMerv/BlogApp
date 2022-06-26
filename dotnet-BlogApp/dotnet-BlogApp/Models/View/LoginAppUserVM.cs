using System.ComponentModel.DataAnnotations;

namespace dotnet_BlogApp.Models.View
{
    public class LoginAppUserVM
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
