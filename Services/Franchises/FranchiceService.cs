using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Franchises
{
    public class FranchiceService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;

        public FranchiceService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Franchise>> GetAll()
        {
            return await _context.Franchises.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Franchise> GetById(int id)
        {
            var franchise = await _context.Franchises.Include(_ => _.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            return franchise;
        }

        public async Task<Franchise> Create(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task<Franchise> Update(Franchise franchise)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == franchise.Id);
            if (franchise == null)
            {
                throw new FranchiseNotFoundException(franchise.Id);
            }
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task DeleteById(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        

        public async Task<ICollection<Movie>> GetFranchiseMovies(int id)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == id);

            if (foundFranchise == false)
            {
                throw new FranchiseNotFoundException(id);
            }

            return await _context.Movies
                .Where(movie => movie.FranchiseId == id)
                .ToListAsync();
        }

        public async Task UpdateMovies(int[] movieIds, int franchiceId)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == franchiceId);

            if (foundFranchise == false)
            {
                throw new FranchiseNotFoundException(franchiceId);
            }

            List<Movie> movies = movieIds
                .ToList()
                .Select(movieIds => _context.Movies
                .Where(movie => movie.Id == movieIds).First())
                .ToList();

            Franchise franchise = await _context.Franchises
                .Where(franchise => franchise.Id == franchiceId)
                .FirstOrDefaultAsync();

            franchise.Movies = movies;
            _context.Entry(franchise).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
