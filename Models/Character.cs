using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        [Required][MaxLength(250)] public string FullName { get; set; }
        [MaxLength(250)] public string? Alias { get; set; }

        [MaxLength(20)] public string? Gender { get; set; }
        [MaxLength(250)] public string? Picture { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
