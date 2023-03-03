using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required][MaxLength(250)] public string Title { get; set; }
        [MaxLength(250)] public string Genre { get; set; }
        public int? Year { get; set; }
        [MaxLength(250)] public string? Director { get; set; }
        [MaxLength(250)] public string? Picture { get; set; }
        [MaxLength(250)] public string? Trailer { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get;set; }
        public ICollection<Character> Characters { get; set; }   
    }
}
