using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(await _movieService.GetAllMovies());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return await _movieService.GetMovieById(id);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.UpdateMovie(movie);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            return CreatedAtAction("GetMovie", new { id = movie.Id }, await _movieService.AddMovie(movie));

        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovie(id);
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent(); ;
        }
    }
}
