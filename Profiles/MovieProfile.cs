using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.FranchiseDtos;
using MovieCharactersAPI.Models.Dtos.MovieDtos;

namespace MovieCharactersAPI.Profiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Characters, options =>
                options.MapFrom(movieDomain => movieDomain.Characters.Select(character => $"{character.FullName}").ToList()))
                .ForMember(dto => dto.Franchise, options => 
                options.MapFrom(movieDomain => movieDomain.Franchise.Name));
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<EditMovieDto, Movie>();
        }
    }
}
