using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Task<ICollection<Character>> GetMovieCharacters(int id);

        Task UpdateCharacters(int[] characterIds, int id);
    }
}
