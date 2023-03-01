﻿using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos;

namespace MovieCharactersAPI.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task<ICollection<Movie>> GetFranchiseMovies(int id);
        Task UpdateMovies(int[] movieIds, int id);
    }
}