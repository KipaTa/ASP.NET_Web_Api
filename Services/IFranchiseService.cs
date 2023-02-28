
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos;

namespace MovieCharactersAPI.Services
{
    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> GetFranchiseById(int id);
        Task<Franchise> CreateFranchise(Franchise franchise);
        Task DeleteFranchise(int id);
        Task<Franchise> UpdateFranchise(Franchise franchise);
        Task<ICollection<Movie>> GetFranchiseMovies(int id);
        Task UpdateMovies(int[] movieIds, int id);
    }
}
