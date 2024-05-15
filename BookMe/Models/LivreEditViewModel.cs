namespace BookMe.Models
{
    public class LivreEditViewModel
    {
        public Livre Livre { get; set; }
        public List<int> SelectedAuteurIds { get; set; }
        public List<int> SelectedThemesIds { get; set; }
    }
}
