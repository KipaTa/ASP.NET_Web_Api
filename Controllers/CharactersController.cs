using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;

        public CharactersController(ICharacterService characterService, IMovieService movieService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
            _movieService = movieService;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDto>>(await _characterService.GetAll()));
        }

        // GET: api/Characters/5
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        

        // DELETE: api/Characters/5
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
