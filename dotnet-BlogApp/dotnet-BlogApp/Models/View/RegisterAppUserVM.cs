﻿using System.ComponentModel.DataAnnotations;

namespace dotnet_BlogApp.Models.View
{
    public class RegisterAppUserVM
    {
        public string UserName { get; set; } = "Anonymous";
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
        public string Alias { get; set; } = string.Empty;
    }
}
