﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.MovieDtos;
using MovieCharactersAPI.Services.Movies;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Dtos.MovieDtos.MovieDto>>> GetMovies()
        {
            return base.Ok(_mapper.Map<IEnumerable<Models.Dtos.MovieDtos.MovieDto>>(await _movieService.GetAll()));
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Dtos.MovieDtos.MovieDto>> GetMovie(int id)
        {
            try
            {
                return base.Ok(_mapper.Map <Models.Dtos.MovieDtos.MovieDto >(await _movieService.GetById(id)));
            }
            catch (MovieNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Movie>> PostMovie(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Models.Movie>(createMovieDto);
            await _movieService.Create(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);

        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, EditMovieDto editMovieDto)
        {
            if (id != editMovieDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.Update(_mapper.Map<Models.Movie>(editMovieDto));
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

        // DELETE: api/Movies/5
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
