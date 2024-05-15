using System.ComponentModel.DataAnnotations;

namespace BookMe.Models
{
    public class Theme
    {
        [Key]
        public int ThemeId { get; set; }

        public string? Libelle { get; set; }
        public ICollection<LivreTheme>? LivreThemes { get; set; }


    }
}
