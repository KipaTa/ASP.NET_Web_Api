using System;
using System.Collections.Generic;
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
using MovieCharactersAPI.Services.Characters;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
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
        public async Task<ActionResult<Character>> CreateCharacter(CreateCharacterDto createCharacterDto)
        {
            var character = _mapper.Map<Character>(createCharacterDto);
            await _characterService.Create(character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
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
