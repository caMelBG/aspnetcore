using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace huncho.Models.Identity
{
    public class LoginModel
    {
        [Required]
        [Remote("IsUserExists", "Account", ErrorMessage = "User with this name dosent exists.")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
