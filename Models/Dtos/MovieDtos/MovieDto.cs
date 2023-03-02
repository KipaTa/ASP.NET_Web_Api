using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models.Dtos.MovieDtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Genre { get; set; }
        public int? Year { get; set; }

        public string? Director { get; set; }

        public string? Picture { get; set; }

        public string? Trailer { get; set; }

        public string Franchise { get; set; }

        public List<string> Characters { get; set; }
    }
}
