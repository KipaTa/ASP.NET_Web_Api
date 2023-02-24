using Microsoft.EntityFrameworkCore;

namespace MovieCharactersAPI.Models
{
    public class MovieCharactersDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Character> Characters { get; set;}
        public DbSet<Franchise> Franchises { get;set;}

        public MovieCharactersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
