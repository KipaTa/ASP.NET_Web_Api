using MovieCharactersAPI.Models;

namespace CharacterCharactersAPI.Services
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<Character> AddCharacter(Character character);
        Task DeleteCharacter(int id);
        Task<Character> UpdateCharacter(Character character);
    }
}
