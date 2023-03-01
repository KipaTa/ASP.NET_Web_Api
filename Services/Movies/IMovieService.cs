using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Task<ICollection<Character>> GetMoviesCharacters(int id);
    }
}
