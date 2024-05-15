namespace BookMe.Models
{
    public class LivreCreateViewModel
    {
        public Livre Livre { get; set; }
        public List<int> SelectedAuteurIds { get; set; }
        public List<int> SelectedThemesIds { get; set; }
    }
}