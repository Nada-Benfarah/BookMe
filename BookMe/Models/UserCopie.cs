namespace BookMe.Models
{
    public class UserCopie
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int CopieId { get; set; }
        public Copie? Copie { get; set;}

        public DateTime? DateDePret { get; set; }
        public DateTime? DatePrevusRendre { get; set; }

        public DateTime? DateDeRendre { get; set; }

        public int Note { get; set; }

    }
}
