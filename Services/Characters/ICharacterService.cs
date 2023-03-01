using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
    }
}
