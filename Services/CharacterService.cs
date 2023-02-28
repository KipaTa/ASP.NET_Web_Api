﻿using CharacterCharactersAPI.Services;
using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieCharactersDbContext _context;

        public CharacterService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<Character> AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new CharacterNotFoundException(id);
            }
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                throw new CharacterNotFoundException(id);
            }

            return character;
        }

        public async Task<Character> UpdateCharacter(Character character)
        {
            var foundCharacter = await _context.Characters.AnyAsync(x => x.Id == character.Id);
            if (character == null)
            {
                throw new CharacterNotFoundException(character.Id);
            }
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return character;
        }
    }
}