using System.ComponentModel.DataAnnotations;

namespace BookMe.Models
{
    public class Copie
    {
        [Key]
        public int CopieId { get; set; }
        public string? Etat { get;set; }
        public string? Edition { get; set; }

        public int LivreId { get; set; }
        [Required]
        public Livre? Livre { get; set;}

        public ICollection<UserCopie>? UserCopies { get; set; }

    }
}
