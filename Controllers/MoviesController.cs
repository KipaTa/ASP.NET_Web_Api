using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.CharacterDtos;
using MovieCharactersAPI.Models.Dtos.MovieDtos;
using MovieCharactersAPI.Services.Movies;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <returns>List of Movies</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            return base.Ok(_mapper.Map<IEnumerable<MovieDto>>(await _movieService.GetAll()));
        }

        /// <summary>
        /// Gets a movie based on a unique identifier
        /// </summary>
        /// <param name="id">A unique id for a movie</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            try
            {
                return base.Ok(_mapper.Map <MovieDto >(await _movieService.GetById(id)));
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="createMovieDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            await _movieService.Create(movie);

            var newMovie = _mapper.Map<MovieDto>(movie);
            newMovie.Franchise = movie?.Franchise?.Name;
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, newMovie);
        }

        /// <summary>
        /// Updates a movie based on a unique identifier
        /// </summary>
        /// <param name="id">A unique id for a movie</param>
        /// <param name="editMovieDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, EditMovieDto editMovieDto)
        {
            if (id != editMovieDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.Update(_mapper.Map<Movie>(editMovieDto));
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

        /// <summary>
        /// Gets all characters in a unique movie
        /// </summary>
        /// <param name="id">A unique id for a franchise</param>
        /// <returns></returns>
        [HttpGet("{id}/chracters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetMovieCharacters(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterDto>>(await _movieService.GetMovieCharacters(id)));
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    });
            }
        }

        /// <summary>
        /// Updates characters to a unique movie
        /// </summary>
        /// <param name="characterIds">A unique id for a character</param>
        /// <param name="id">A unique id for a movie</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharactersForMovie(int[] characterIds, int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {

                await _movieService.UpdateCharacters(characterIds, id);
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

        /// <summary>
        /// Deletes a unique movie
        /// </summary>
        /// <param name="id">A unique id for a movie</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteById(id);
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
