using System.ComponentModel.DataAnnotations;

namespace BookMe.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]  
            public string UserName { get; set; } = string.Empty;
        [Required]  
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; }

        public ICollection<UserCopie>? UserCopies { get; set; }






    }
}
