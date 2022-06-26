using System.ComponentModel.DataAnnotations;

namespace dotnet_BlogApp.Models.View
{
    public class DeleteAppUserVM
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
