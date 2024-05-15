namespace BookMe.Models
{
    public class LivreTheme
    {
        public int LivreId { get; set; }
        public Livre? Livre { get; set; }
        public int ThemeId { get; set; }
        public Theme? Theme { get; set; }

    }
}
