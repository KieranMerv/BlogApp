using System.ComponentModel.DataAnnotations;

namespace dotnet_BlogApp.Models.View
{
    public class UpdateAppUserVM
    {
        public string? UserName { get; set; }
        public string? Alias { get; set; }
        public string? Email { get; set; }
        public string? NewEmail { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
    }
}
