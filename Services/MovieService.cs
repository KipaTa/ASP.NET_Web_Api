﻿using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
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
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                throw new MovieNotFoundException(id);
            }

            return movie;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.Movies.Add(movie);
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
    }
}
