using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.User
{
    public class UserRegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4, ErrorMessage ="UserName must be at least 4 characters long")]
        public string Username { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
