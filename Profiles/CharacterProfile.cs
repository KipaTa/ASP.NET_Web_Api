using AutoMapper;
using MovieCharactersAPI.Models.Dtos.FranchiseDtos;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.CharacterDtos;

namespace MovieCharactersAPI.Profiles
{
    public class CharacterProfile: Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(characterDomain => characterDomain.Movies.Select(movie => $"{movie.Title}").ToList()));
            CreateMap<CreateCharacterDto, CreateCharacterDto>();

        }
    }
}
