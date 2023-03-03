using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.CharacterDtos;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task<List<string>> UpdateJoinTable(Character character, List<int> movieIds);
    }
}
