using System.ComponentModel.DataAnnotations;

namespace AnNet_GestContact.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }   

        [Required]
        public string Password { get; set; }
    }
}
