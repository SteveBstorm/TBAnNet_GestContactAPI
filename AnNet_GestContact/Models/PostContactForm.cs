using System.ComponentModel.DataAnnotations;

namespace AnNet_GestContact.Models
{
    public class PostContactForm
    {
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(384)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
