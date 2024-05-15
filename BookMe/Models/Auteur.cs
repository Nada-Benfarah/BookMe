using System.ComponentModel.DataAnnotations;

namespace BookMe.Models
{
    public class Auteur
    {
        [Key]
        public int AuteurId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Nationalitie { get; set; }

        public ICollection<AuteurLivre>? AuteurLivres { get; set; } 
    }
}
