using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using MovieCharactersAPI.Services.Characters;
using MovieCharactersAPI.Services.Movies;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

       
        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns>List of Characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDto>>(await _characterService.GetAll()));
        }

        /// <summary>
        /// Gets a character based on a unique identifier
        /// </summary>
        /// <param name="id">A unique id for a character</param>
        /// <returns>A character resource</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacterById(int id)
        {
            try
            {
                Console.WriteLine("Yritysta");
                return Ok(_mapper.Map<CharacterDto>(await _characterService.GetById(id)));
            }
            catch (CharacterNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="newCharacterDto"></param>
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> CreateCharacter(NewCharacterDto newCharacterDto)
        {
            List<int> movieIds = newCharacterDto.Movies;
            List<string> linkedMovies = new List<string>();

            CreateCharacterDto createCharacterDto = new CreateCharacterDto()
            {
                FullName = newCharacterDto.FullName,
                Alias = newCharacterDto.Alias,
                Gender = newCharacterDto.Gender,
                Picture = newCharacterDto.Picture,
            };

            var character = _mapper.Map<Character>(createCharacterDto);
            await _characterService.Create(character);

            linkedMovies.AddRange(await _characterService.UpdateJoinTable(character, movieIds));

            var characterDto = _mapper.Map<CharacterDto>(character);
            characterDto.Movies= linkedMovies;

            return CreatedAtAction(nameof(GetCharacterById), new { id = characterDto.Id }, characterDto);
        }

        /// <summary>
        /// Updates a character based a unique identifier
        /// </summary>
        /// <param name="id">A unique id for a character</param>
        /// <param name="editCharacterDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, EditCharacterDto editCharacterDto)
        {
            if (id != editCharacterDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _characterService.Update(_mapper.Map<Character>(editCharacterDto));
            }
            catch (CharacterNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }

            return NoContent();
        }



        /// <summary>
        /// Deletes a unique character
        /// </summary>
        /// <param name="id">A unique id for a character</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteById(id);
            }
            catch (CharacterNotFoundException ex)
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
