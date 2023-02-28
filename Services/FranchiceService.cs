﻿using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public class FranchiceService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;

        public FranchiceService(MovieCharactersDbContext context)
        {
            _context = context;
        }
        public async Task<Franchise> CreateFranchise(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _context.Franchises.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Franchise> GetFranchiseById(int id)
        {
            var franchise = await _context.Franchises.Include(_ => _.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            return franchise;
        }

        public async Task<Franchise> UpdateFranchise(Franchise franchise)
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


        public async Task DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                throw new FranchiseNotFoundException(id);
            }
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovies(int[] movieIds, int franchiceId)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == franchiceId);

            if (foundFranchise == null)
            {
                throw new FranchiseNotFoundException(franchiceId);
            }
        }
    }
}