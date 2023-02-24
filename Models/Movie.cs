namespace MovieCharactersAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }

        public string Director { get; set; }

        public string Picture { get; set; }

        public string Trailer { get; set; }

        public int FranchiseId { get; set; }

        public Franchise Franchise { get;set; }

        public ICollection<Character> Characters { get; set; }   

    }
}
