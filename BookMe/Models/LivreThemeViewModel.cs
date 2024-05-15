namespace BookMe.Models
{
    public class LivreThemeViewModel
    {
        public IEnumerable<BookMe.Models.Livre> Livres { get; set; }
        public IEnumerable<BookMe.Models.Theme> Themes { get; set; }
        public IEnumerable<Auteur> Auteurs { get; set; }
        public string AuteurName { get; set; }
        public string ThemeLibelle { get; set; }
        public string LivreName { get; set; }

    }
}
