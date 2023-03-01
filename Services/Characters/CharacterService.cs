using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieCharactersDbContext _context;

        public CharacterService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetById(int id)
        {
            var character = await _context.Characters.Include(_ => _.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (character == null)
            {
                throw new CharacterNotFoundException(id);
            }

            return character;
        }

        public async Task<Character> Create(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            return character;
        }
        public async Task<Character> Update(Character character)
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


        public async Task DeleteById(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new CharacterNotFoundException(id);
            }
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }
    }
}
