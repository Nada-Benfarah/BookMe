using System.ComponentModel.DataAnnotations;

namespace BookMe.Models
{
    public class Livre
    {
        [Key]
        public int LivreId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]

        public string? Language { get; set; }
        [Required]
        [Range(1,9000, ErrorMessage = "pagesNumber must be between 1 and 9000")]
        public int pagesNumbers { get; set; }
        [Required]

        public string? ImagePath { get; set; }
        [Required]
        public string? Description { get; set; }
        public ICollection<AuteurLivre>? AuteurLivres { get; set; }  
        public ICollection<Copie>? Copies { get; set; }

        public ICollection<LivreTheme> LivreThemes { get; set; } = new List<LivreTheme>();






    }
}
