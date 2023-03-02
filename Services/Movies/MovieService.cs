using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MovieCharactersDbContext _context;

        public MovieService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.Include(x => x.Characters).Include(m => m.Franchise).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _context.Movies.Include(x => x.Characters).Include(m => m.Franchise).FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
            {
                throw new MovieNotFoundException(id);
            }

            return movie;
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Update(Movie movie)
        {
            var foundMovie = await _context.Movies.AnyAsync(x => x.Id == movie.Id);
            if (movie == null)
            {
                throw new MovieNotFoundException(movie.Id);
            }
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task DeleteById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new MovieNotFoundException(id);
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Character>> GetMovieCharacters(int id)
        {

            var foundMovies = await _context.Movies.AnyAsync(x => x.Id == id);

            if (foundMovies == false)
            {
                throw new FranchiseNotFoundException(id);
            }

            return await _context.Characters
                .Where(character => character.Movies.Any(movie => movie.Id == id))
                .Include(x => x.Movies)
                .ToListAsync();
        }

        public  async Task UpdateCharacters(int[] characterIds, int movieId)
        {
            var foundMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);

            if (foundMovie == false)
            {
                throw new MovieNotFoundException(movieId);
            }
            
            List<Character> characters = characterIds
                .ToList()
                .Select(characterIds => _context.Characters
                .Where(character => character.Id == characterIds).First())
                .ToList();

            Movie movie = await _context.Movies
                .Where(movie => movie.Id == movieId)
                .FirstOrDefaultAsync();

            movie.Characters = characters;
            _context.Entry(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
