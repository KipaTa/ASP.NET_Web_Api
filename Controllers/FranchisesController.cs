using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.CharacterDtos;
using MovieCharactersAPI.Models.Dtos.FranchiseDtos;
using MovieCharactersAPI.Models.Dtos.MovieDtos;
using MovieCharactersAPI.Services.Franchises;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all franchises
        /// </summary>
        /// <returns>List of Franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDto>>(await _franchiseService.GetAll()));
        }

        /// <summary>
        /// Gets a franchise based on a unique identifier
        /// </summary>
        /// <param name="id"> A unique id for a franchise</param>
        /// <returns>A franchise resource</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchiseById(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDto>(await _franchiseService.GetById(id)));
            }
            catch(FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Creates a new franchise
        /// </summary>
        /// <param name="createFranchiseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> CreateFranchise(CreateFranchiseDto createFranchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(createFranchiseDto);
            await _franchiseService.Create(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = franchise.Id }, franchise);
        }


        /// <summary>
        /// Updates a franchise based on a unique identifier
        /// </summary>
        /// <param name="id">A unique id for a franchise</param>
        /// <param name="editFranchiseDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, EditFranchiseDto editFranchiseDto)
        {

            if (id != editFranchiseDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _franchiseService.Update(_mapper.Map<Franchise>(editFranchiseDto));
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Gets all movies in a unique franchise
        /// </summary>
        /// <param name="id">A unique id for a franchise</param>
        /// <returns></returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetFranchiseMovies(int id)
        {
            try
            {
                return Ok(
                    _mapper.Map<List<MovieDto>>(
                        await _franchiseService.GetFranchiseMovies(id)
                        )
                    );
            }
            catch (FranchiseNotFoundException ex)
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
        /// Gets all characters in all movies in a unique franchise
        /// </summary>
        /// <param name="id">A unique id for a franchise</param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetFranchiseCharacters(int id)
        {
            try
            {
               var franchises = await _franchiseService.GetFranchiseCharacters(id);

                List<CharacterDto> movies = new List<CharacterDto>();

                foreach (var franchise in franchises)
                {
                    movies = _mapper.Map<List<CharacterDto>>(franchise.Characters);  
                }

                return Ok(movies);

            }
            catch (FranchiseNotFoundException ex)
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
        /// Updates a unique movie to a unique franchise
        /// </summary>
        /// <param name="movieIds">A unique id for a movie</param>
        /// <param name="id">A unique id for a franchise</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesForFranchise(int[] movieIds, int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
         
                await _franchiseService.UpdateMovies(movieIds, id);
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();

        }



        /// <summary>
        /// Deletes a unique franchise
        /// </summary>
        /// <param name="id">A unique id for a franchise</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteById(id);
            }
            catch (FranchiseNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();

        }
    }
}
